namespace SocialWeb.Models
{
	public class GroupChat
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<GroupChatMessage>? GroupChatMessages { get; set; }
		public ICollection<GroupChatMember>? GroupChatMembers { get; set; }

	}
}
