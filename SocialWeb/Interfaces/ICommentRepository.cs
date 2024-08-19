using SocialWeb.Models;

namespace SocialWeb.Interfaces
{
	public interface ICommentRepository
	{
		List<Comment> GetCommentsByPostId(int postid);
		bool CreateComment(Comment comment);
	}
}
