using SocialWeb.Models;

namespace SocialWeb.Interfaces
{
	public interface IGroupChatRepository
	{
        GroupChat CreateGroupChat(string name);
        List<GroupChat> GetGroupChatByUserId(int userid);
        GroupChat GetGroupChatById(int id);
    }
}
