using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChrisConnorBlogAssessment.Models
{
    /// <summary>
    /// Outlines structure for a Blog category
    /// </summary>
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter a category Name")]
        public string CategoryName { get; set; }

        public Category()
        {
            this.Blogs = new List<Blog>();
        }
        public virtual ICollection<Blog> Blogs { get; set; }

    }
}