using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChrisConnorBlogAssessment.Models;

namespace ChrisConnorBlogAssessment.ViewModels
{
    /// <summary>
    /// ViewModel to display a list of blogs alongside a list of categories
    /// </summary>
    public class BlogsCategoriesViewModel
    {
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}