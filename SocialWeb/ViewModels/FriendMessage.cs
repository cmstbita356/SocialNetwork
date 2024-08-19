using SocialWeb.Models;

namespace SocialWeb.ViewModels
{
    public class FriendMessage
    {
        public User Friend { get; set; }
        public List<PrivateMessage> Messages { get; set; }
    }
}
