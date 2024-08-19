namespace SocialWeb.Interfaces
{
	public interface ILikeRepository
	{
		bool LikePost(int userid, int postid);
		bool UnLikePost(int userid, int postid);
		bool isLike(int userid, int postid);
		int CountLike(int postid);
	}
}
