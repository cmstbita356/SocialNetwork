using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;
using SocialWeb.Unit_Of_Work;
using SocialWeb.ViewModels;
using System.Security.Claims;

namespace SocialWeb.Controllers
{
    public class HomeController : Controller
	{
		private IUnitOfWork UnitOfWork;

		public HomeController(SocialContext context)
		{
            UnitOfWork = new UnitOfWork(context);
		}
        private User GetCurrentUser()
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            return UnitOfWork.UserRepository.GetUser(username);
        }
        [Authorize]
		public IActionResult Index()
		{
            User currentUser = GetCurrentUser();
            var friends = UnitOfWork.FriendRepository.GetFriendRequest(currentUser.Id);
            List<User> friendRequests = new List<User>();
            foreach(var f in friends)
            {
                friendRequests.Add(UnitOfWork.UserRepository.GetUser(f.UserId1));
            }
            HomeIndex model = new HomeIndex
            {
                FriendRequest = friendRequests
            };
			return View(model);
		}
		public IActionResult Register()
		{
			return View();
		}
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();
        }
        

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (UnitOfWork.UserRepository.IsUsernameExisted(user.Username))
            {
                ModelState.AddModelError("Username", "Username already exists. Please choose another one.");
            }
            if (ModelState.IsValid)
            {
                UnitOfWork.UserRepository.Register(user);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password, bool Remember)
        {
            if (UnitOfWork.UserRepository.Login(Username, Password))
            {
                User user = UnitOfWork.UserRepository.GetUser(Username);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Remember
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("validation","Username or Password is incorrect");
                return View();
            }
        }
        [HttpPost]
        public IActionResult LoadFriendRequest()
        {
            var currentUser = GetCurrentUser();
            var friends = UnitOfWork.FriendRepository.GetFriendRequest(currentUser.Id);
            List<User> friendRequests = new List<User>();
            foreach (var f in friends)
            {
                friendRequests.Add(UnitOfWork.UserRepository.GetUser(f.UserId1));
            }
            return PartialView("_FriendRequestPartial", friendRequests);
        }
        [HttpPost]
        public IActionResult DeleteRequest(int friendid)
        {
            var currentUser = GetCurrentUser();
            UnitOfWork.FriendRepository.DeleteRequest(currentUser.Id, friendid);

            return Ok();
        }
        [HttpPost]
        public IActionResult AcceptRequest(int friendid)
        {
            var currentUser = GetCurrentUser();
            UnitOfWork.FriendRepository.AcceptRequest(currentUser.Id, friendid);

            return Ok();
        }
    }

}

