    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
    using System.Web.Mvc;

namespace ChrisConnorBlogAssessment.Models
{
/// <summary>
/// Outlines structure for a blog posts
/// </summary>
    public class Blog
    {
        
        [Key]
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Please enter a Blog Title"), Display(Name = "Blog Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the content of your Blog."),DataType(DataType.MultilineText),AllowHtml]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateModified { get; set; }

        [Display(Name = "Publish Blog?")]
        public bool IsPublished { get; set; }

        public Blog()
        {
            this.Comments = new List<Comment>();
        }

        [Display(Name = "Blog Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name="Author")]
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}