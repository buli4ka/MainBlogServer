using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.DatabaseConfig;
using Server.Exceptions;
using Server.Models.UserModels;
using Server.Utils;
using Server.Utils.UserValidation;
using Server.Utils.Jwt;
using Server.ViewModels.UserViewModels;

namespace Server.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly Context _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            IOptions<AppSettings> appSettings,
            Context context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username);


            if (user == null || !UserCheck.ValidatePassword(model.HashedPassword, user.HashedPassword))
                throw new AppException("Username or password is incorrect");

            var response = _mapper.Map<AuthenticateResponse>(user);
            response.JwtToken = _jwtUtils.GenerateToken(user);
            return response;
        }

        public async Task<AuthenticateResponse> Register(RegisterRequest model)
        {
            if (await _context.Users.AnyAsync(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            if (await _context.Users.AnyAsync(x => x.Email == model.Email))
                throw new AppException("Email '" + model.Email + "' is already taken");

            var user = _mapper.Map<User>(model);

            if (!UserCheck.IsUserValid(user))
                throw new AppException("Not valid user data");

            user.HashedPassword = UserCheck.HashPassword(model.HashedPassword);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.JwtToken = _jwtUtils.GenerateToken(user);
            return response;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<AuthorView>> GetUserSubscribers(Guid userId)
        {
            var user = await _context.Users
                .Where(p => p.Id == userId)
                .Include(user => user.Subscribers)
                .ThenInclude(user => user.UserIcon)
                .FirstOrDefaultAsync();

            if (user?.Subscribers == null)
            {
                throw new KeyNotFoundException("No users have been found");
            }

            var subscribers = new List<AuthorView>();
            foreach (var subscriber in user.Subscribers)
            {
                var sub = _mapper.Map<AuthorView>(subscriber);
                sub.IconUrl = _appSettings.AppUrl + "UserIcon/getById/" + subscriber.UserIcon.Id;
                subscribers.Add(sub);
            }

            return subscribers;
        }

        public async Task<IEnumerable<AuthorView>> GetUserSubscribed(Guid userId)
        {
            var user = await _context.Users
                .Where(p => p.Id == userId)
                .Include(user => user.Subscribed)
                .ThenInclude(user => user.UserIcon)
                .FirstOrDefaultAsync();

            if (user?.Subscribed == null)
            {
                throw new KeyNotFoundException("No users have been found");
            }

            var subscribedUsers = new List<AuthorView>();
            foreach (var subscribed in user.Subscribed)
            {
                var sub = _mapper.Map<AuthorView>(subscribed);
                sub.IconUrl = _appSettings.AppUrl + "UserIcon/getById/" + subscribed.UserIcon.Id;
                subscribedUsers.Add(sub);
            }

            return subscribedUsers;
        }

        public async Task<AuthorView> GetAuthorById(Guid authorId)
        {
            var user = await _context.Users
                .Where(user => user.Id == authorId)
                .Include(user => user.Subscribed)
                .Include(user => user.Subscribers)
                .Include(user => user.Posts)
                .ThenInclude(post => post.PostComments)
                .Include(user => user.Posts)
                .ThenInclude(post => post.PostLikes)
                .Include(user => user.Posts)
                .ThenInclude(post => post.PostImages)
                .Include(user => user.UserIcon)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (user == null) throw new KeyNotFoundException("User not found");

            var response = _mapper.Map<AuthorView>(user);

            return response;
        }

        public async Task<UserView> GetUserById(Guid userId)
        {
            var user = await _context.Users
                .Where(user => user.Id == userId)
                .Include(user => user.Subscribed)
                .Include(user => user.Subscribers)
                .Include(user => user.Posts)
                .Include(user => user.LikedPosts)
                .Include(user => user.UserIcon)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (user == null) throw new KeyNotFoundException("User not found");
            return _mapper.Map<UserView>(user);
        }


        public async Task<UserView> Update(Guid id, UpdateRequest model)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException("Internal server error - user not found");

            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            if (!string.IsNullOrEmpty(model.HashedPassword))
                user.HashedPassword = UserCheck.HashPassword(model.HashedPassword);

            _mapper.Map(model, user);
            
            if (!UserCheck.IsUserValid(user))
                throw new AppException("Not valid user data");
            
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserView>(user);
        }

        public async Task Delete(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task Subscribe(SubscribeRequest model)
        {
            var user = await _context.Users
                .Where(u => u.Id == model.UserId)
                .Include(u => u.Subscribers)
                .Include(u => u.Subscribed)
                .FirstOrDefaultAsync();

            var author = await _context.Users
                .Where(a => a.Id == model.AuthorId)
                .Include(a => a.Subscribers)
                .Include(a => a.Subscribed)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }

            var subscribed = user.Subscribed != null
                ? new List<User>(user.Subscribed) {author}
                : new List<User> {author};
            user.Subscribed = subscribed;

            var subscribers = author.Subscribers != null
                ? new List<User>(author.Subscribers) {user}
                : new List<User> {user};
            author.Subscribers = subscribers;


            _context.Users.UpdateRange(user, author);
            await _context.SaveChangesAsync();
        }

        public async Task Unsubscribe(SubscribeRequest model)
        {
            var user = await _context.Users
                .Where(u => u.Id == model.UserId)
                .Include(u => u.Subscribers)
                .Include(u => u.Subscribed)
                .FirstOrDefaultAsync();

            var author = await _context.Users
                .Where(a => a.Id == model.AuthorId)
                .Include(a => a.Subscribers)
                .Include(a => a.Subscribed)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }

            var subscribed = new List<User>(user.Subscribed);
            user.Subscribed.Remove(author);
            var subscribers = new List<User>(author.Subscribers);
            subscribers.Remove(user);

            user.Subscribed = subscribed;
            author.Subscribers = subscribers;


            _context.Users.UpdateRange(user, author);
            await _context.SaveChangesAsync();
        }
    }
}