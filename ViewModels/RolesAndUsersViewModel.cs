using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChrisConnorBlogAssessment.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChrisConnorBlogAssessment.ViewModels
{
    /// <summary>
    /// ViewModels used to store roles and users to display on Users controller
    /// </summary>
    public class RolesAndUsersViewModel
    {
        public ICollection<UserAndRolesViewModel> Users { get; set; }
        public ICollection<IdentityRole> Roles { get; set; }
    }

    public class UserAndRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public List<RoleViewModel> Roles { get; set; }

    }

    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}