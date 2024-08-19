using SocialWeb.Models;

namespace SocialWeb.Interfaces
{
	public interface IGroupChatMessageRepository
	{
        bool AddMessage(GroupChatMessage message);
        List<GroupChatMessage> GetMessages(int groupchatid);
       
    }
}
