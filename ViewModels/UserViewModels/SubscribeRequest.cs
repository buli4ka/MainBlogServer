using System;

namespace Server.ViewModels.UserViewModels
{
    public class SubscribeRequest
    {
        public Guid UserId { get; set; }
        public Guid AuthorId { get; set; }
    }
}