namespace SocialWeb.Models
{
	public class PrivateMessage
	{
		public int MessageId { get; set; }
		public int SenderId { get; set; }
		public User? Sender { get; set; }
		public int RecieverId { get; set; }
		public User? Reciever { get; set; }
		public string Content { get; set; }
		public DateTime Date { get; set; }
	}
}
