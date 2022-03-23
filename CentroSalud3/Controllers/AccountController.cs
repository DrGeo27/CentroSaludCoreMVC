using CentroSalud3.DbContext;
using CentroSalud3.Models;
using CentroSalud3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentroSalud3.Controllers
{
    //[Authorize(Roles = "Administrador")]
    public class AccountController : Controller
    {
        #region ConexionBD
        private AmbulatorioDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            AmbulatorioDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }
        #endregion ConexionBD

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }
        #endregion Register

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(vm.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Medico"))
                    {
                        return RedirectToAction("Index", "Medico");
                    }
                    else if (roles.Contains("Enfermera"))
                    {
                        return RedirectToAction("Index", "Enfermera");
                    }
                    else if (roles.Contains("Paciente"))
                    {
                        return RedirectToAction("Index", "Paciente");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Login Failure");
            }
            return View(vm);
        }
        #endregion Login

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion Logout

        #region Index
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var users = await _db.Users.ToListAsync();
            var roles = await _db.Roles.ToListAsync();
            var usersinroles = await _db.UserRoles.ToListAsync();

            List<AccountAllUserViewModel> listaUsuariosEnRole = new List<AccountAllUserViewModel>();
            
            foreach (var uinr in usersinroles)
            {
                AccountAllUserViewModel vm = new AccountAllUserViewModel();

                var currentUser = await _userManager.FindByIdAsync(uinr.UserId);
                var currentRole = await _roleManager.FindByIdAsync(uinr.RoleId);

                vm.User = currentUser;
                vm.Role = currentRole;

                listaUsuariosEnRole.Add(vm);
            }

            return View(listaUsuariosEnRole.ToList());
        }
        #endregion Index

        #region AccessDenied
        [HttpGet]
        [Route("/Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion AccessDenied
    }
}
