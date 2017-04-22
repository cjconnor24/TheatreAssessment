using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChrisConnorBlogAssessment.ViewModels
{
    /// <summary>
    /// ViewModel used to create a Blog whilst restricting access to Author and DateTime fields
    /// </summary>
    public class CreateBlogViewModel
    {
        [Required(ErrorMessage = "Please enter a Blog title"),Display(Name = "Blog Name")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter some content for the blog"),Display(Name = "Blog Content"),DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }
        [Display(Name = "Publish Blog to Site?")]
        public bool IsPublished { get; set; }
        public int CategoryId { get; set; }
        public int? BlogId { get; set; }
        
    }
}