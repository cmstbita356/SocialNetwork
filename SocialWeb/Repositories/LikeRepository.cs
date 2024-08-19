using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;

namespace SocialWeb.Repositories
{
	public class LikeRepository : ILikeRepository
	{
		private SocialContext context;

		public LikeRepository(SocialContext context)
		{
			this.context = context;
		}

		public int CountLike(int postid)
		{
			List<Like> likes = context.Like.Where(l => l.PostId == postid).ToList();
			return likes.Count;
		}

		public bool isLike(int userid, int postid)
		{
			return context.Like.Any(l => l.UserId == userid && l.PostId == postid);
		}

		public bool LikePost(int userid, int postid)
		{
			Like like = new Like()
			{
				UserId = userid,
				PostId = postid
			};
			context.Like.Add(like);
			if (context.SaveChanges() > 0) return true;
			return false;
		}

		public bool UnLikePost(int userid, int postid)
		{
			var like = context.Like.Where(l => l.UserId == userid && l.PostId == postid).FirstOrDefault();
			context.Like.Remove(like);
			if (context.SaveChanges() > 0) return true;
			return false;
		}
	}
}
