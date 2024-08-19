using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;

namespace SocialWeb.Repositories
{
	public class UserRepository : IUserRepository
	{
		private SocialContext context;

		public UserRepository(SocialContext context)
		{
			this.context = context;
		}

        public bool IsUsernameExisted(string username)
        {
            return context.Users.Any(u => u.Username == username);
        }

        public bool Login(string username, string password)
        {
            bool check = context.Users.Any(u => u.Username == username && u.Password == password);

            return check;
        }
        public User GetUser(int id)
        {
            return context.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        public User GetUser(string username)
        {
            return context.Users.Where(u => u.Username == username).FirstOrDefault();
        }
        public bool Register(User user)
        {
            context.Users.Add(user);
            if (context.SaveChanges() > 0) return true;
            return false;
        }

        public bool EditNewName(int userid, string newName)
        {
            var user = context.Users.Where(u => u.Id == userid).FirstOrDefault();
            user.Name = newName;
            if (context.SaveChanges() > 0) return true;
            return false;
        }

		public bool EditNewImage(int userid, byte[] newimg)
		{
			var user = context.Users.Where(u => u.Id == userid).FirstOrDefault();
			user.Image = newimg;
			if (context.SaveChanges() > 0) return true;
			return false;
		}

		public List<User> GetUserByName(string word)
		{
            return context.Users.Where(u => u.Name.Contains(word)).ToList();
		}
	}
}
