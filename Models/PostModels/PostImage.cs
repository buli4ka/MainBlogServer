using System;

namespace Server.Models.PostModels
{
    public class PostImage : BaseImageModel
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}