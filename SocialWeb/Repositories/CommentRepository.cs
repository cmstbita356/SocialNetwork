using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;

namespace SocialWeb.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private SocialContext context;
		
		public CommentRepository(SocialContext context)
		{
			this.context = context;
		}

		public bool CreateComment(Comment comment)
		{
			context.Comments.Add(comment);
			if (context.SaveChanges() > 0) return true;
			return false;
		}

		public List<Comment> GetCommentsByPostId(int postid)
		{
			return context.Comments.Where(c => c.PostId == postid).ToList();
		}
	}
}
