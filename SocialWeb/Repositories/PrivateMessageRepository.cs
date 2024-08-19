using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;

namespace SocialWeb.Repositories
{
    public class PrivateMessageRepository : IPrivateMessageRepository
	{
		private SocialContext context;

		public PrivateMessageRepository(SocialContext context)
		{
			this.context = context;
		}

		public bool AddMessage(PrivateMessage message)
		{
			context.PrivateMessages.Add(message);
			if (context.SaveChanges() > 0) return true;
			return false;
		}

		public List<PrivateMessage> GetMessages(int userid, int friendid)
        {
            
            var listPM = context.PrivateMessages.Where(pm => pm.SenderId == userid && pm.RecieverId == friendid).ToList();
            var listPM2 = context.PrivateMessages.Where(pm => pm.SenderId == friendid && pm.RecieverId == userid).ToList();
            List<PrivateMessage> result = listPM.Concat(listPM2)
                                            .OrderBy(pm => pm.Date)
                                            .ToList();
            return result;
        }
    }
}
