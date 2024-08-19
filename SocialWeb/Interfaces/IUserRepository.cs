using SocialWeb.Models;

namespace SocialWeb.Interfaces
{
	public interface IUserRepository
	{	
		bool IsUsernameExisted(string username);
		bool Register(User user);
		bool Login(string username, string password);
		User GetUser(int id);
		User GetUser(string username);
		List<User> GetUserByName(string word);
		bool EditNewName(int userid, string newName);
		bool EditNewImage(int userid, byte[] newimg);
    }
}
