using CentroSalud3.DbContext;
using CentroSalud3.Models;
using CentroSalud3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RoleController : Controller
    {
        #region ConexionBD
        private AmbulatorioDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public RoleController(AmbulatorioDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion ConexionBD

        #region Index
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        #endregion Index

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion Create

        #region AddUserRole
        public async Task<IActionResult> AddUserRole(string id)
        {
            var roleDisplay = await _db.Roles.Select(r => new
            {
                Id = r.Id,
                Value = r.Name
            }).ToListAsync();

            RoleAddUserRoleViewModel vm = new RoleAddUserRoleViewModel();

            var user = await _userManager.FindByIdAsync(id);
            vm.User = user;

            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserRole(RoleAddUserRoleViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.User.Id);
            var role = await _roleManager.FindByIdAsync(vm.Role);
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            var roleDisplay = _db.Roles.Select(r => new
            {
                r.Id,
                Value = r.Name
            }).ToList();

            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");

            return View(vm);
        }
        #endregion AddUserRole

        #region DeleteUserRole
        public async Task<IActionResult> DeleteUserRole(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.RemoveFromRoleAsync(user, role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
        #endregion DeleteUserRole
    }
}
