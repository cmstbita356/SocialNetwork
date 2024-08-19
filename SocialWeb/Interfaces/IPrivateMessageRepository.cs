using SocialWeb.Models;

namespace SocialWeb.Interfaces
{
	public interface IPrivateMessageRepository
	{
		List<PrivateMessage> GetMessages(int userid, int friendid);
		bool AddMessage(PrivateMessage message);
	}
}
