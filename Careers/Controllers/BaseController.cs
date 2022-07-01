using Careers.DataAccess.Models;
using Careers.DataAccess.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class BaseController : Controller
    {
        protected SignInManager<User> _signInManager;
        protected UserManager<User> _userManager;
        protected IUnitOfWork _unitOfWork;

        public BaseController(SignInManager<User> signInManager,
                             UserManager<User> userManager,
                             IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

    }
}
