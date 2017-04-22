using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ChrisConnorBlogAssessment.Models;

namespace ChrisConnorBlogAssessment.ViewModels
{
    /// <summary>
    /// ViewModel used to display a Blog
    /// </summary>
    public class BlogDisplayViewModel
    {
        public Blog Blog { get; set; }
        [Required(ErrorMessage = "Please enter a comment"),Display(Name = "Comment"),AllowHtml]
        public string Content { get; set; }
        public int BlogId { get; set; }
    }
}