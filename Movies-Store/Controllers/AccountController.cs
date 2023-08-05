using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Store.Data;
using Movies_Store.Data.ViewModels;
using Movies_Store.Models;

namespace Movies_Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly CinemaContext cinemaContext;
        public AccountController( CinemaContext _cinemaContext,UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            cinemaContext = _cinemaContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Users()
        {
           var users= await cinemaContext.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login()
        {
            return View(new LogInVM());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LogInVM logInVM)
        {
            if(!ModelState.IsValid) 
            {
                return View(logInVM);
            }
            var loginUser = await userManager.FindByEmailAsync(logInVM.EmailAddress);
            if (loginUser != null)
            {
                var checkPassword=await userManager.CheckPasswordAsync(loginUser, logInVM.Password);
                if (checkPassword)
                {
                    var result=await signInManager.PasswordSignInAsync(loginUser,logInVM.Password,true,true);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movie");
                    }
                }
                ModelState.AddModelError("Password", "Wrong password");
                return View(loginUser);
            }
            ModelState.AddModelError("EmailAddress", "Invalid email address");
            return View(loginUser);
        }

        public IActionResult Register()
        {
           
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) { return View(registerVM); }
            var newUser = await userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (newUser != null)
            {
                ModelState.AddModelError("EmailAddress", "The Email is already existed");
            }

            newUser=new ApplicationUser()
            {
                FullName=registerVM.FullName,
                UserName=registerVM.UserName,
                Email=registerVM.EmailAddress,
               
            };
            IdentityResult result=await userManager.CreateAsync(newUser,registerVM.Password);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, "User");
                return RedirectToAction("Login");

            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
            }
            return View(registerVM);
            
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
