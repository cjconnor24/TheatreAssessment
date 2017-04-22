using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChrisConnorBlogAssessment.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        //[Key]
        //public int OwnerID { get; set; }
        [Required(ErrorMessage = "Please enter the first name")]
        public string Forename { get; set; }
        [Required(ErrorMessage = "Please enter the last name")]
        public string Surname { get; set; }

        //[Required(ErrorMessage = "Please enter a Postcode")]
        //public string Postcode { get; set; }
        //[Required(ErrorMessage = "Please enter the first line of the address")]
        //public string Street { get; set; }
        //public string Town { get; set; }
        /* ADDED */
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }

        public ApplicationUser()
        {
            this.Comments = new List<Comment>();
            this.Blogs = new List<Blog>();
            this.IsActive = true;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


}