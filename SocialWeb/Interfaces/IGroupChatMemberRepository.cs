using SocialWeb.Models;

namespace SocialWeb.Interfaces
{
	public interface IGroupChatMemberRepository
	{
		void AddMember(int groupChatId, int userid);
		List<GroupChatMember> GetMembers(int groupChatId);
		bool IsMember(int groupchatId, int userid);
	}
}
