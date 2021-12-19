using System;
using System.Collections.Generic;
using System.Linq;
using Server.Models.PostModels;
using Server.Models.UserModels;

namespace Server.DatabaseConfig
{
    public class SampleData
    {
        public static void InitData(Context context)
        {
            if (!context.Users.Any())
            {
              
                var user1 = new User
                {
                    FirstName = "Alan",
                    LastName = "Wake",
                    Username = "a",
                    Email = "AlanWake@gmail.com",
                    Biography =
                        "Is a bestselling crime fiction author suffering from a two-year stretch of writer's block",
                    IsPrivate = false,
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword("a"),
                };
                var user2 = new User
                {
                    FirstName = "Tony",
                    LastName = "Stark",
                    Username = "t",
                    Email = "TonyStark@gmail.com",
                    Biography = "bilinear, playboy, philanthropist",
                    IsPrivate = true,
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword("t"),
                };
                var user3 = new User
                {
                    FirstName = "Peter",
                    LastName = "Parker",
                    Username = "p",
                    Biography = "Became a spider man",
                    Email = "PeterParker@gmail.com",
                    IsPrivate = false,
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword("p"),
                    Subscribers = new List<User> {user1, user2},
                    Subscribed = new List<User> {user1, user2},
                };
                var user4 = new User
                {
                    FirstName = "Steve",
                    LastName = "Jobs",
                    Username = "s",
                    Biography = "An American business magnate, industrial designer, investor, and media proprietor.",
                    Email = "SteveJobs@gmail.com",
                    IsPrivate = false,
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword("s"),
                    Subscribers = new List<User> {user1, user2, user3},
                    Subscribed = new List<User> {user1, user2, user3},
                };

                var post1 = new Post
                {
                    Title = "Quick js programming",
                    Text =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus eget purus augue. Phasellus ultricies id eros vitae feugiat. Proin luctus ultrices sagittis. Praesent quis risus nunc. Aenean vehicula lectus at neque ullamcorper, eu vulputate justo scelerisque. Etiam nec consequat lectus, non faucibus dolor. Fusce eget mi porttitor, laoreet massa ut, faucibus enim. Curabitur at tortor non elit tincidunt tincidunt in et ex. Nam accumsan hendrerit luctus. Praesent aliquam est pharetra ex tempor fringilla. Fusce venenatis maximus diam et aliquam. Sed eget turpis nisi. Vestibulum tincidunt, sapien ut rhoncus euismod, elit neque sollicitudin sapien, id tincidunt neque mi eu turpis.",
                    Author = user1,
                    CreatedAt = DateTime.Now,
                };

                var post2 = new Post
                {
                    Title = "Quick python programming",
                    Text =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus eget purus augue. Phasellus ultricies id eros vitae feugiat. Proin luctus ultrices sagittis. Praesent quis risus nunc. Aenean vehicula lectus at neque ullamcorper, eu vulputate justo scelerisque. Etiam nec consequat lectus, non faucibus dolor. Fusce eget mi porttitor, laoreet massa ut, faucibus enim. Curabitur at tortor non elit tincidunt tincidunt in et ex. Nam accumsan hendrerit luctus. Praesent aliquam est pharetra ex tempor fringilla. Fusce venenatis maximus diam et aliquam. Sed eget turpis nisi. Vestibulum tincidunt, sapien ut rhoncus euismod, elit neque sollicitudin sapien, id tincidunt neque mi eu turpis.",
                    Author = user2,
                    CreatedAt = DateTime.Now,
                };

                var post3 = new Post
                {
                    Title = "Quick C++ programming",
                    Text =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus eget purus augue. Phasellus ultricies id eros vitae feugiat. Proin luctus ultrices sagittis. Praesent quis risus nunc. Aenean vehicula lectus at neque ullamcorper, eu vulputate justo scelerisque. Etiam nec consequat lectus, non faucibus dolor. Fusce eget mi porttitor, laoreet massa ut, faucibus enim. Curabitur at tortor non elit tincidunt tincidunt in et ex. Nam accumsan hendrerit luctus. Praesent aliquam est pharetra ex tempor fringilla. Fusce venenatis maximus diam et aliquam. Sed eget turpis nisi. Vestibulum tincidunt, sapien ut rhoncus euismod, elit neque sollicitudin sapien, id tincidunt neque mi eu turpis.",
                    Author = user3,
                    CreatedAt = DateTime.Now,
                };
                var post4 = new Post
                {
                    Title = "Quick Ruby programming",
                    Text =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus eget purus augue. Phasellus ultricies id eros vitae feugiat. Proin luctus ultrices sagittis. Praesent quis risus nunc. Aenean vehicula lectus at neque ullamcorper, eu vulputate justo scelerisque. Etiam nec consequat lectus, non faucibus dolor. Fusce eget mi porttitor, laoreet massa ut, faucibus enim. Curabitur at tortor non elit tincidunt tincidunt in et ex. Nam accumsan hendrerit luctus. Praesent aliquam est pharetra ex tempor fringilla. Fusce venenatis maximus diam et aliquam. Sed eget turpis nisi. Vestibulum tincidunt, sapien ut rhoncus euismod, elit neque sollicitudin sapien, id tincidunt neque mi eu turpis.",
                    Author = user4,
                    CreatedAt = DateTime.Now,
                };

                var postComment1 = new PostComment
                {
                    Text = "Cool post!!",
                    User = user3,
                    Post = post1
                };

                var postComment2 = new PostComment
                {
                    Text = "Cool post!!",
                    User = user4,
                    Post = post2
                };

                var postComment3 = new PostComment
                {
                    Text = "not interesting post...",
                    User = user4,
                    Post = post1
                };

                var postComment4 = new PostComment
                {
                    Text = "not interesting post...",
                    User = user1,
                    Post = post3
                };

                var postLike1 = new PostLike
                {
                    User = user1,
                    Post = post1
                };
                var postLike2 = new PostLike
                {
                    User = user2,
                    Post = post1
                };
                var postLike3 = new PostLike
                {
                    User = user3,
                    Post = post1
                };
                var postLike4 = new PostLike
                {
                    User = user4,
                    Post = post1
                };

                var postLike5 = new PostLike
                {
                    User = user1,
                    Post = post2
                };
                var postLike6 = new PostLike
                {
                    User = user2,
                    Post = post2
                };
                var postLike7 = new PostLike
                {
                    User = user3,
                    Post = post2
                };
                var postLike8 = new PostLike
                {
                    User = user4,
                    Post = post2
                };

                context.AddRange(
                    user1, user2, user3, user4
                    , post1, post2, post3, post4
                    , postComment1, postComment2, postComment3, postComment4
                    , postLike1, postLike2, postLike3, postLike4, postLike5, postLike6, postLike7, postLike8);
            }


            context.SaveChanges();
        }
    }
}