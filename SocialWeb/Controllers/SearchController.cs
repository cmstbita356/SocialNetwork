using Microsoft.AspNetCore.Mvc;
using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;
using SocialWeb.Unit_Of_Work;
using SocialWeb.ViewModels;
using System.Security.Claims;

namespace SocialWeb.Controllers
{
	public class SearchController : Controller
	{
		private IUnitOfWork UnitOfWork;
        public SearchController(SocialContext context)
        {
			UnitOfWork = new UnitOfWork(context);   
        }
        public IActionResult Index()
		{
			return View();
		}
        public IActionResult AcceptRequest()
        {
			return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteRequest()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
		public IActionResult Index(string word)
		{
			var currentUser = GetCurrentUser();
			List<User> users = UnitOfWork.UserRepository.GetUserByName(word);
			List<User> usersList = new List<User>();
			foreach(var user in users)
			{
				if (user.Id != currentUser.Id) usersList.Add(user);
			}

			List<SearchIndex> result = new List<SearchIndex>();
			foreach(var user in usersList)
			{
				SearchIndex searchIndex = new SearchIndex();
                if (UnitOfWork.FriendRepository.GetStatus(user.Id, currentUser.Id) == "Pending")
				{
					searchIndex.User = user;
					searchIndex.Status = "Requested";
				}
				else
				{
                    searchIndex.User = user;
                    searchIndex.Status = UnitOfWork.FriendRepository.GetStatus(currentUser.Id, user.Id);
                }
				result.Add(searchIndex);

            }
			return View(result);
		}
		[HttpPost]
		public IActionResult RequestFriend(int friendid)
		{
			User currentUser = GetCurrentUser();
			UnitOfWork.FriendRepository.RequestFriend(currentUser.Id, friendid);

			return Ok();
		}
		[HttpPost]
		public IActionResult AcceptRequest(int friendid)
		{
            var currentUser = GetCurrentUser();
            UnitOfWork.FriendRepository.AcceptRequest(currentUser.Id, friendid);

            return RedirectToAction("Index", "Home");
        }
		[HttpPost]
		
		public IActionResult DeleteRequest(int friendid)
		{
            var currentUser = GetCurrentUser();
            UnitOfWork.FriendRepository.DeleteRequest(currentUser.Id, friendid);

            return RedirectToAction("Index", "Home");
        }

        private User GetCurrentUser()
		{
			var username = User.FindFirst(ClaimTypes.Name).Value;
			return UnitOfWork.UserRepository.GetUser(username);
		}
	}
}
