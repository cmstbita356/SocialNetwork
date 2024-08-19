using Microsoft.CodeAnalysis.CSharp.Syntax;
using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;

namespace SocialWeb.Repositories
{
    public class GroupChatMemberRepository : IGroupChatMemberRepository
	{
		private SocialContext context;

		public GroupChatMemberRepository(SocialContext context)
		{
			this.context = context;
		}

        public void AddMember(int groupChatId, int userid)
        {
            if(!IsMember(groupChatId, userid))
            {
                GroupChatMember gcm = new GroupChatMember
                {
                    GroupChatId = groupChatId,
                    UserId = userid
                };
                context.GroupChatMember.Add(gcm);
                context.SaveChanges();
            }
            
        }

        public List<GroupChatMember> GetMembers(int groupChatId)
        {
            List<GroupChatMember> gcm = context.GroupChatMember.Where(e => e.GroupChatId == groupChatId).ToList();
            
            
            return gcm;
        }

        public bool IsMember(int groupchatId, int userid)
        {
            return context.GroupChatMember.Any(gcm => gcm.GroupChatId == groupchatId && gcm.UserId == userid);
        }
    }
}
