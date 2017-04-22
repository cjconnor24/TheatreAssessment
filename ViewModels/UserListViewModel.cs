using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ChrisConnorBlogAssessment.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChrisConnorBlogAssessment.ViewModels
{
    public class UserListViewModel
    {
        public ICollection<ApplicationUser> Users { get; set; }
        public IdentityRole Role { get; set; }
    }
}