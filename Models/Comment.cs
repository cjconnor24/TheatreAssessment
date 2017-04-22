using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChrisConnorBlogAssessment.Models
{
    /// <summary>
    /// Outlines structure for a comments
    /// </summary>
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required(ErrorMessage = "Your comment cannot be blank"), Display(Name = "Comment")]
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        public Comment()
        {
            this.DateAdded = DateTime.Now;
        }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }



    }
}