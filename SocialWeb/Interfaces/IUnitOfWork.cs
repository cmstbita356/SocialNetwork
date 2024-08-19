namespace SocialWeb.Interfaces
{
	public interface IUnitOfWork
	{
		ICommentRepository CommentRepository { get; }
		IFriendRepository FriendRepository { get; }
		IGroupChatMemberRepository GroupChatMemberRepository { get; }
		IGroupChatMessageRepository GroupChatMessageRepository { get; }
		IGroupChatRepository GroupChatRepository { get; }
		ILikeRepository LikeRepository { get; }
		IPostRepository PostRepository { get; }
		IPrivateMessageRepository PrivateMessageRepository { get; }
		IUserRepository UserRepository { get; }
	}
}
