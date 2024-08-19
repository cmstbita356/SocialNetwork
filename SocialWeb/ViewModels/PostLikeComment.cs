using SocialWeb.Models;

namespace SocialWeb.ViewModels
{
	public class PostLikeComment
	{
		public Post Post { get; set; }
		public List<UserComment>? UserComments { get; set; }
		public int CountLike { get; set; }
		public bool isLike { get; set; }
	}
}
