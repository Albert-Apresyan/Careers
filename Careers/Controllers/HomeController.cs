using Careers.DataAccess.Models;
using Careers.DataAccess.ViewModels;
using Careers.DataAccess.Wrappers;
using Careers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Careers.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(SignInManager<User> signInManager,
                                UserManager<User> userManager,
                                IUnitOfWork unitOfWork,
                                ILogger<HomeController> logger
                                )
                                : base(signInManager, userManager, unitOfWork)
        {
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Home()
        {
            //var allInfo = _unitOfWork.UserRepository.GetProfileInfo();
            var user = await _userManager.GetUserAsync(User);

            var userViewModel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegistrationDate = user.RegistrationDate,
                Email = user.Email,
                PostedJobsCount = await _unitOfWork.UserRepository.GetUserPostsCount(user.Id),
                AppliedJobsCount = await _unitOfWork.UserRepository.GetUserAppliedPostsCount(user.Id)
            };
               

            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Home", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(login);
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            User user = new User();
            user.FirstName = register.FirstName;
            user.LastName = register.LastName;
            user.Email = register.Email;
            user.RegistrationDate = DateTime.Now;
            user.UserName = register.Email;

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(register);
            }

            return RedirectToAction("Login", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


    }
}
