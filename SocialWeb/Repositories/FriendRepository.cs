using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;

namespace SocialWeb.Repositories
{
    public class FriendRepository : IFriendRepository
	{
		private SocialContext context;

		public FriendRepository(SocialContext context)
		{
			this.context = context;
		}
        private Friend GetFriend(int userid1, int userid2)
        {
            return context.Friend.Where(f => f.UserId1 == userid2 && f.UserId2 == userid1).FirstOrDefault();

        }
        public bool AcceptRequest(int yourselfid, int friendid)
        {
            var friend = context.Friend.Where(f => f.UserId1 == friendid && f.UserId2 == yourselfid).FirstOrDefault();
            friend.Status = "Accepted";
            context.SaveChanges();
            Friend f = new Friend
            {
                UserId1 = yourselfid,
                UserId2 = friendid,
                Status = "Accepted"
            };
            context.Friend.Add(f);
            if (context.SaveChanges() > 0) return true;
            return false;
        }

        public int CountFriends(int userid)
        {
            var friendList = context.Friend.Where(f => f.UserId1 == userid && f.Status == "Accepted").ToList();

			return friendList.Count;
        }

        public bool DeleteRequest(int yourselfid, int friendid)
        {
            var f = GetFriend(friendid, yourselfid);
            context.Friend.Remove(f);
            if (context.SaveChanges() > 0) return true;
            return false;
        }

        public string GetStatus(int userid1, int userid2)
        {
            var friend = context.Friend.Where(f => f.UserId1 == userid1 && f.UserId2 == userid2).FirstOrDefault();
            if (friend == null) return "None";
            return friend.Status;
        }

        public bool RequestFriend(int userid1, int userid2)
        {
            Friend f = new Friend()
            {
                UserId1 = userid1,
                UserId2 = userid2,
                Status = "Pending"
            };
            context.Friend.Add(f);
            if (context.SaveChanges() > 0) return true;
            return false;
        }

        public List<Friend> GetFriendRequest(int userid)
        {
            var f = context.Friend.Where(f => f.UserId2 == userid && f.Status == "Pending").ToList();
            return f;
        }

		public List<Friend> GetFriend(int userid)
		{
			return context.Friend.Where(f => f.UserId1 == userid && f.Status == "Accepted").ToList();

		}
	}
}
