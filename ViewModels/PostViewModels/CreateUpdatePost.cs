using System;
using System.ComponentModel.DataAnnotations;
using Server.Models;

namespace Server.ViewModels.PostViewModels
{
    public class CreateUpdatePost : BaseModel
    {
        [Required] public string Title { get; set; }
        [Required] public string Text { get; set; }

        [Required] public Guid AuthorId { get; set; }
    }
}