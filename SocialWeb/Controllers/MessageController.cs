using Microsoft.AspNetCore.Mvc;
using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;
using SocialWeb.Unit_Of_Work;
using SocialWeb.ViewModels;
using System.Security.Claims;

namespace SocialWeb.Controllers
{
	public class MessageController : Controller
	{
		private IUnitOfWork UnitOfWork;
        public MessageController(SocialContext context)
        {
			UnitOfWork = new UnitOfWork(context);
        }
		private User GetYourself()
		{
			int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			return UnitOfWork.UserRepository.GetUser(id);
		}
        public IActionResult Index()
		{
			User yourself = GetYourself();
            List<Friend> listfriend = UnitOfWork.FriendRepository.GetFriend(yourself.Id);
            List<User> friends = new List<User>();
            foreach (var f in listfriend)
            {
                User u = UnitOfWork.UserRepository.GetUser(f.UserId2);
                friends.Add(u);
            }

			MessageIndex model = new MessageIndex()
			{
				Yourself = yourself,
				Friends = friends,
				GroupChats = UnitOfWork.GroupChatRepository.GetGroupChatByUserId(yourself.Id)
			};
			return View(model);
		}
		public IActionResult CreateGroupChat()
		{
			return RedirectToAction("Index");
		}
        public IActionResult AddMember()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
		public IActionResult LoadPrivateMessage(int friendid)
		{
			User yourself = GetYourself();
			List<PrivateMessage> listpm = UnitOfWork.PrivateMessageRepository.GetMessages(yourself.Id, friendid);
			FriendMessage model = new FriendMessage
			{
				Friend = UnitOfWork.UserRepository.GetUser(friendid),
				Messages = listpm
			};
			return PartialView("_PrivateMessagePartial", model);
		}
		[HttpPost]
		public IActionResult AddPrivateMessage(int friendid, string content)
		{
			if(content != null && content != string.Empty)
			{
				User yourself = GetYourself();
				PrivateMessage pm = new PrivateMessage
				{
					SenderId = yourself.Id,
					RecieverId = friendid,
					Content = content,
					Date = DateTime.Now
				};
				UnitOfWork.PrivateMessageRepository.AddMessage(pm);
				return Ok();
			}
			return BadRequest();	

		}
		[HttpPost]
		public IActionResult CreateGroupChat(string nameGroup)
		{
			User yourself = GetYourself();
			GroupChat gc = UnitOfWork.GroupChatRepository.CreateGroupChat(nameGroup);
			UnitOfWork.GroupChatMemberRepository.AddMember(gc.Id, yourself.Id);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult LoadGroupMessage(int groupid)
		{
			User yourself = GetYourself();


            List<GroupChatMember> gcm = UnitOfWork.GroupChatMemberRepository.GetMembers(groupid);
			List<User> members = new List<User>();
			foreach (var i in gcm)
			{
				members.Add(UnitOfWork.UserRepository.GetUser(i.UserId));
			}

			List<Friend> friends = UnitOfWork.FriendRepository.GetFriend(yourself.Id);
			List<User> usersToAdd = new List<User>();
			foreach(var f in friends)
			{
				User u = UnitOfWork.UserRepository.GetUser(f.UserId2);
				if (!members.Contains(u)) usersToAdd.Add(u);
			}

			List<GroupChatMessage> Messages = UnitOfWork.GroupChatMessageRepository.GetMessages(groupid);
			List<UserGroupMessage> userGroupMessages = new List<UserGroupMessage>();
			foreach(var m in Messages)
			{
				UserGroupMessage ugm = new UserGroupMessage
				{
					Sender = UnitOfWork.UserRepository.GetUser(m.SenderId),
					Message = m,
				};
				userGroupMessages.Add(ugm);
            }

            GroupMessage model = new GroupMessage()
			{
				GroupChat = UnitOfWork.GroupChatRepository.GetGroupChatById(groupid),
				Members = members,
                UserGroupMessages = userGroupMessages,
				Yourself = yourself,
				UsersToAdd = usersToAdd,
            };
			

			return PartialView("_GroupMessagePartial", model);
		}
		[HttpPost]
		public IActionResult AddGroupMessage(int groupid, string content)
		{
            if (content != null && content != string.Empty)
            {
                User yourself = GetYourself();
                GroupChatMessage gm = new GroupChatMessage
                {
                    SenderId = yourself.Id,
                    GroupChatId = groupid,
                    Content = content,
                    Date = DateTime.Now
                };
                UnitOfWork.GroupChatMessageRepository.AddMessage(gm);
                return Ok();
            }
            return BadRequest();
        }
		[HttpPost]
		public IActionResult AddMember(int groupchatid, List<int> selectedFriends)
		{
			if(selectedFriends != null || selectedFriends.Count != 0)
			{
				foreach(int i in selectedFriends)
				{
					UnitOfWork.GroupChatMemberRepository.AddMember(groupchatid, i);
				}
			}
			return RedirectToAction("Index");
		}
    }
}
