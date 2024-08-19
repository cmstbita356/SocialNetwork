using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SocialWeb.Models;

namespace SocialWeb.Data
{
	public class SocialContext : DbContext
	{
        public SocialContext(DbContextOptions<SocialContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<GroupChat> GroupChats { get; set; }
		public DbSet<GroupChatMessage> GroupChatMessages { get; set; }
		public DbSet<GroupChatMember> GroupChatMember { get; set; }
		public DbSet<PrivateMessage> PrivateMessages { get; set; }
		public DbSet<Friend> Friend { get; set; }
		public DbSet<Like> Like { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//User
			modelBuilder.Entity<User>(e =>
			{
				e.HasKey(u => u.Id);
				e.HasIndex(u => u.Username).IsUnique();

				e.HasMany(u => u.Posts)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId);

				e.HasMany(u => u.Comments)
				.WithOne(c => c.User)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.NoAction);

				e.HasMany(u => u.GroupChatMessages)
				.WithOne(gcm => gcm.User)
				.HasForeignKey(gcm => gcm.SenderId);

				e.HasMany(u => u.PrivateSenderMessages)
				.WithOne(pm => pm.Sender)
				.HasForeignKey(pm => pm.SenderId)
				.OnDelete(DeleteBehavior.NoAction);

				e.HasMany(u => u.PrivateRecieverMessages)
				.WithOne(pm => pm.Reciever)
				.HasForeignKey(pm => pm.RecieverId)
				.OnDelete(DeleteBehavior.NoAction);
			});

			//Post
			modelBuilder.Entity<Post>(e =>
			{
				e.HasKey(p => p.Id);

				e.HasMany(p => p.Comments)
				.WithOne(c => c.Post)
				.HasForeignKey(c => c.PostId);
			});

			//Comment
			modelBuilder.Entity<Comment>(e =>
			{
				e.HasKey(c => c.Id);
			});

			//GroupChat
			modelBuilder.Entity<GroupChat>(e =>
			{
				e.HasKey(c => c.Id);
				e.HasMany(gc => gc.GroupChatMessages)
				.WithOne(gcm => gcm.GroupChat)
				.HasForeignKey(gcm => gcm.GroupChatId);
			});

			//GroupChatMember
			modelBuilder.Entity<GroupChatMember>(e =>
			{
				e.HasKey(gcm => new { gcm.UserId, gcm.GroupChatId });
				e.HasOne(gcm => gcm.User)
				.WithMany(u => u.GroupChatMembers)
				.HasForeignKey(gcm => gcm.UserId);

				e.HasOne(gcm => gcm.GroupChat)
				.WithMany(gc => gc.GroupChatMembers)
				.HasForeignKey(gcm => gcm.GroupChatId);
			});

			//GroupChatMessage
			modelBuilder.Entity<GroupChatMessage>(e =>
			{
				e.HasKey(c => c.MessageId);
			});

			//PrivateMessage
			modelBuilder.Entity<PrivateMessage>(e =>
			{
				e.HasKey(c => c.MessageId);
			});

			//Friends
			modelBuilder.Entity<Friend>(e =>
			{
				e.HasKey(f => new { f.UserId1, f.UserId2 });

				e.HasOne<User>().WithMany()
				.HasForeignKey(f => f.UserId1)
				.OnDelete(DeleteBehavior.NoAction);

				e.HasOne<User>().WithMany()
				.HasForeignKey(f => f.UserId2)
				.OnDelete(DeleteBehavior.NoAction);
			});

			//Like
			modelBuilder.Entity<Like>(e =>
			{
				e.HasKey(f => new { f.PostId, f.UserId });

				e.HasOne<User>().WithMany()
				.HasForeignKey(f => f.UserId)
				.OnDelete(DeleteBehavior.NoAction);

				e.HasOne<Post>().WithMany()
				.HasForeignKey(f => f.PostId)
				.OnDelete(DeleteBehavior.NoAction);
			});
		}
	}
	public class SocialContextFactory : IDesignTimeDbContextFactory<SocialContext>
	{
		public SocialContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<SocialContext>();
			optionsBuilder.UseSqlServer("Data Source=.\\Bita;Initial Catalog=SocialWeb;Integrated Security=True;Trust Server Certificate=True");

			return new SocialContext(optionsBuilder.Options);
		}
	}

}
