using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Repositories;

namespace SocialWeb.Unit_Of_Work
{
	public class UnitOfWork : IUnitOfWork
	{
		private SocialContext context;
        public UnitOfWork(SocialContext context)
        {
            this.context = context;
        }
		private ICommentRepository commentRepository;
		public ICommentRepository CommentRepository
		{
			get
			{
				if(commentRepository == null)
				{
					commentRepository = new CommentRepository(context);
				}
				return commentRepository;
			}
		}
		private IFriendRepository friendRepository;
		public IFriendRepository FriendRepository
		{
			get
			{
				if (friendRepository == null)
				{
					friendRepository = new FriendRepository(context);
				}
				return friendRepository;
			}
		}
		private IGroupChatMemberRepository groupChatMemberRepository;
		public IGroupChatMemberRepository GroupChatMemberRepository
		{
			get
			{
				if (groupChatMemberRepository == null)
				{
					groupChatMemberRepository = new GroupChatMemberRepository(context);
				}
				return groupChatMemberRepository;
			}
		}
		private IGroupChatMessageRepository groupChatMessageRepository;
		public IGroupChatMessageRepository GroupChatMessageRepository
		{
			get
			{
				if(groupChatMessageRepository == null)
				{
					groupChatMessageRepository = new GroupChatMessageRepository(context);
				}
				return groupChatMessageRepository;
			}
		}
		private IGroupChatRepository groupChatRepository;
		public IGroupChatRepository GroupChatRepository
		{
			get
			{
				if (groupChatRepository == null)
				{
					groupChatRepository = new GroupChatRepository(context);
				}
				return groupChatRepository;
			}
		}
		private ILikeRepository likeRepository;
		public ILikeRepository LikeRepository
		{
			get
			{
				if (likeRepository == null)
				{
					likeRepository = new LikeRepository(context);
				}
				return likeRepository;
			}
		}
		private IPostRepository postRepository;
		public IPostRepository PostRepository
		{
			get
			{
				if (postRepository == null)
				{
					postRepository = new PostRepository(context);
				}
				return postRepository;
			}
		}
		private IPrivateMessageRepository privateMessageRepository;
		public IPrivateMessageRepository PrivateMessageRepository
		{
			get
			{
				if (privateMessageRepository == null)
				{
					privateMessageRepository = new PrivateMessageRepository(context);
				}
				return privateMessageRepository;
			}
		}
		private IUserRepository userRepository;
		public IUserRepository UserRepository
		{
			get
			{
				if (userRepository == null)
				{
					userRepository = new UserRepository(context);
				}
				return userRepository;
			}
		}
	}
}
