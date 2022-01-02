using System.Linq;
using Server.Models.UserModels;
using Server.ViewModels.UserViewModels;
using AutoMapper;
using Server.Models.PostModels;
using Server.ViewModels.PostViewModels;

namespace Server.Utils
{
    public class AutoMapperProfile : Profile
    {
        private const string AppDomain = "https://localhost:5001/api/";

        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<User, AuthenticateResponse>();

            CreateMap<PostLike, PostLikeView>();

            CreateMap<PostLike, UserLikeView>();
            
            CreateMap<CreateUpdateCommentView, PostComment>();

            CreateMap<PostComment, PostCommentView>();
                // .ForMember("PostSubComments",
                //     opt => opt.MapFrom(comment=>
                //         comment.PostSubComments.Select(i)));
            
            // CreateMap<PostSubComment, PostSubCommentView>();
            CreateMap<AddLike, PostLike>();


            CreateMap<CreateUpdatePost, Post>();

            CreateMap<RegisterRequest, User>();

            CreateMap<Post, PostView>()
                .ForMember("ImageUrls",
                    opt => opt.MapFrom(post =>
                        post.PostImages.Select(i => AppDomain + "Image/getById/" + i.Id))); // todo change to _appSettings
            
            CreateMap<Post, PostPreview>()
                .ForMember("PreviewImage",
                    opt => opt.MapFrom(post =>
                        AppDomain + "Image/getById/" +
                        post.PostImages.FirstOrDefault().Id)) // todo change to _appSettings
                .ForMember("QuantityOfComments",
                    opt => opt.MapFrom(post =>
                        post.PostComments.Count))
                .ForMember("QuantityOfLikes",
                    opt => opt.MapFrom(post =>
                        post.PostLikes.Count));

            CreateMap<CreateUpdatePost, Post>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        if (prop == null) return false;
                        return prop.GetType() != typeof(string) || !string.IsNullOrEmpty((string) prop);
                    }
                ));
            
            CreateMap<User, AuthorPreview>()
                .ForMember("IconUrl",
                    opt => opt.MapFrom(user =>
                        AppDomain + "Icon/getById/" + user.UserIcon.Id)); // todo change to _appSettings

            CreateMap<User, UserView>()
                .ForMember("IconUrl",
                    opt => opt.MapFrom(user =>
                        AppDomain + "Icon/getById/" + user.UserIcon.Id)); // todo change to _appSettings
            

            CreateMap<User, AuthorView>()
                .ForMember("IconUrl",
                    opt => opt.MapFrom(user =>
                        AppDomain + "Icon/getById/" + user.UserIcon.Id)) // todo change to _appSettings
                .ForMember("QuantityOfSubscribers",
                    opt => opt.MapFrom(user =>
                        user.Subscribers.Count))
                .ForMember("QuantityOfSubscribed",
                    opt => opt.MapFrom(user =>
                        user.Subscribed.Count));


            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        if (prop == null) return false;
                        return prop.GetType() != typeof(string) || !string.IsNullOrEmpty((string) prop);
                    }
                ));
            
           
        }
    }
}