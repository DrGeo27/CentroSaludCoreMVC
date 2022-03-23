using CentroSalud3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.ViewModels
{
    public class RoleAddUserRoleViewModel
    {
        public ApplicationUser User { get; set; }

        [Display(Name = "Rol")]
        public string Role { get; set; }

        public SelectList RoleList { get; set; }
    }
}
