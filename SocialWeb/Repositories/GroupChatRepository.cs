using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;
using System.Text.RegularExpressions;

namespace SocialWeb.Repositories
{
    public class GroupChatRepository : IGroupChatRepository
	{
		private SocialContext context;

		public GroupChatRepository(SocialContext context)
		{
			this.context = context;
		}

        public GroupChat CreateGroupChat(string name)
        {
            GroupChat gc = new GroupChat()
            {
                Name = name,
            };
            context.GroupChats.Add(gc);
            context.SaveChanges();

            // Lấy lại GroupChat vừa tạo bằng cách truy vấn dựa trên ID
            return context.GroupChats.Find(gc.Id);
        }

        public List<GroupChat> GetGroupChatByUserId(int userid)
        {
            List<int> listgroupid = context.GroupChatMember.Where(gcm => gcm.UserId == userid).Select(gcm => gcm.GroupChatId).ToList();
            List<GroupChat> result = new List<GroupChat>();
            foreach(int i in listgroupid)
            {
                result.Add(GetGroupChatById(i));
            }
            return result;
        }

        public GroupChat GetGroupChatById(int id)
        {
            return context.GroupChats.Find(id);
        }

    }
}
