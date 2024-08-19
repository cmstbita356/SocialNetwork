using System.Threading.Channels;

namespace SocialWeb.Models
{
	public class GroupChatMessage
	{
		public int MessageId { get; set; }
		public int SenderId { get; set; }
		public User? User { get; set; }
		public int GroupChatId { get; set; }
		public GroupChat? GroupChat { get; set; }
		public string Content { get; set; }
		public DateTime Date { get; set; }
	}
}
