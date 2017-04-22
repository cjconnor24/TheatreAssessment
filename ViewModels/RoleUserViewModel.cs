using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChrisConnorBlogAssessment.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChrisConnorBlogAssessment.ViewModels
{
    /// <summary>
    /// ViewModel to hold each type of user, staff and administration users.
    /// </summary>
    public class RoleUserViewModel
    {
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<ApplicationUser> Staff { get; set; }
        public ICollection<ApplicationUser> Admin { get; set; }

    }
}