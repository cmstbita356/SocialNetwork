using System.ComponentModel.DataAnnotations;

namespace SocialWeb.Models
{
	public class User
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please enter your username")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Please enter your password")]
		public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

		public byte[]? Image { get; set; }
		public ICollection<Post>? Posts { get; set; }
		public ICollection<Comment>? Comments { get; set; }
		public ICollection<GroupChatMember>? GroupChatMembers { get; set; }
		public ICollection<GroupChatMessage>? GroupChatMessages { get; set; }
		public ICollection<PrivateMessage>? PrivateSenderMessages { get; set; }
		public ICollection<PrivateMessage>? PrivateRecieverMessages { get; set; }

	}
}
