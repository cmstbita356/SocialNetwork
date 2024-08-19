namespace SocialWeb.Models
{
	public class GroupChatMember
	{
		public int UserId { get; set; }
		public int GroupChatId { get; set; }
		public User? User { get; set; }
		public GroupChat? GroupChat { get; set; }
	}
}
