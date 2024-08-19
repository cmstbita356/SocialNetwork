using SocialWeb.Models;

namespace SocialWeb.ViewModels
{
    public class UserIndex
    {
        public User CurrentUser { get; set; }
        public User User { get; set; }
        public List<PostLikeComment> PostLikeComments { get; set; }
        public List<User> Friends { get; set; }
        public int CountFriends { get; set; }
        
    }
}
