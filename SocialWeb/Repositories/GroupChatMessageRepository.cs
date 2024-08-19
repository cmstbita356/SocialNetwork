using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;

namespace SocialWeb.Repositories
{
	public class GroupChatMessageRepository : IGroupChatMessageRepository
	{
		private SocialContext context;

		public GroupChatMessageRepository(SocialContext context)
		{
			this.context = context;
		}
        public bool AddMessage(GroupChatMessage message)
        {
            context.GroupChatMessages.Add(message);
            if (context.SaveChanges() > 0) return true;
            return false;
        }

        public List<GroupChatMessage> GetMessages(int groupchatid)
        {

            var listGM = context.GroupChatMessages.Where(gm => gm.GroupChatId == groupchatid).ToList();
            
            return listGM;
        }
    }
}
