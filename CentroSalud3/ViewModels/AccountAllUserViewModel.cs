using CentroSalud3.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.ViewModels
{
    public class AccountAllUserViewModel
    {
        public ApplicationUser User { get; set; }
        public IdentityRole Role { get; set; }
    }
}
