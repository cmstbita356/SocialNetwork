namespace SocialWeb.Models
{
	public class Post
	{
		public int Id { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
		public DateTime CreateDate {  get; set; }
		public byte[]? Image { get; set; }
		public User? User { get; set; }
		public ICollection<Comment>? Comments { get; set; }
    }
}
