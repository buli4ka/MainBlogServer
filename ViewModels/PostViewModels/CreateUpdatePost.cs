using System;
using System.ComponentModel.DataAnnotations;

namespace Server.ViewModels.PostViewModels
{
    public class CreateUpdatePost
    {
        [Required] public string Title { get; set; }
        [Required] public string Text { get; set; }

        public Guid AuthorId { get; set; }
    }
}