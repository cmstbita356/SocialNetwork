using SocialWeb.Models;

namespace SocialWeb.Interfaces
{
	public interface IFriendRepository
	{
		List<Friend> GetFriendRequest(int userid);
		List<Friend> GetFriend(int userid);
		string GetStatus(int userid1, int userid2);
		int CountFriends(int userid);
		bool RequestFriend(int userid1, int userid2);
		bool DeleteRequest(int userid1, int userid2);
		bool AcceptRequest(int userid1, int userid2);
	}
}
