using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChrisConnorBlogAssessment.ViewModels
{
    /// <summary>
    /// ViewModel used to create a comment
    /// </summary>
    public class CommentViewModel
    {
        [Required(ErrorMessage = "Please enter a comment"),Display(Name = "Comment")]
        public string Content { get; set; }

        public int BlogId  { get; set; }
    }
}