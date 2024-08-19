using SocialWeb.Models;

namespace SocialWeb.ViewModels
{
    public class MessageIndex
    {
        public User Yourself { get; set; }
        public List<User> Friends { get; set; }
        public List<GroupChat> GroupChats { get; set; }

    }
    
}
