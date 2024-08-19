using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;

namespace SocialWeb.Repositories
{
    public class PostRepository : IPostRepository
	{
		private SocialContext context;

		public PostRepository(SocialContext context)
		{
			this.context = context;
		}

        public bool Create(Post post)
        {
            context.Posts.Add(post);
			if (context.SaveChanges() > 0) return true;
			return false;
        }

        public bool DeletePost(int id)
        {
            var post = context.Posts.Where(p => p.Id == id).FirstOrDefault();
            context.Posts.Remove(post);

            var likes = context.Like.Where(l => l.PostId == id).ToList();
            foreach(var like in likes)
            {
                context.Like.Remove(like);
            }

            if (context.SaveChanges() > 0) return true;
            return false;
        }

        public List<Post> GetAllByUserId(int userid)
        {
            List<Post> posts = context.Posts.Where(p => p.UserId == userid).ToList();
            return posts;
        }

        public Post GetPostById(int id)
        {
            return context.Posts.Where(p => p.Id == id).FirstOrDefault();
        }

    }
}
