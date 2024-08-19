using SocialWeb.Models;

namespace SocialWeb.ViewModels
{
    public class GroupMessage
    {
        public User Yourself { get; set; }
        public GroupChat GroupChat { get; set; }
        public List<UserGroupMessage> UserGroupMessages { get; set; }
        public List<User> Members { get; set; }
        public List<User> UsersToAdd { get; set; }
    }
    public class UserGroupMessage
    {
        public User Sender { get; set; }
        public GroupChatMessage Message { get; set; }
    }
}
