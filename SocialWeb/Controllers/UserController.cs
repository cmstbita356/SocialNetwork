using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SocialWeb.Data;
using SocialWeb.Interfaces;
using SocialWeb.Models;
using SocialWeb.Unit_Of_Work;
using SocialWeb.ViewModels;
using System.Security.Claims;

namespace SocialWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUnitOfWork UnitOfWork;
        
        public UserController(SocialContext context)
        {
            UnitOfWork = new UnitOfWork(context);
        }
        public IActionResult Index(int? id = null)
        {
			var username = User.FindFirst(ClaimTypes.Name).Value;
			var yourself = UnitOfWork.UserRepository.GetUser(username);
			//User currentUser = new User();

			User user = new User();

			if (id == null)
            {
				user = UnitOfWork.UserRepository.GetUser(username);
				//user = currentUser;
			}
            else
            {
                int Id = (int)id;
                user = UnitOfWork.UserRepository.GetUser(Id);
                //currentUser = user;
            }

			List<Post> posts = UnitOfWork.PostRepository.GetAllByUserId(user.Id);
			List<Post> reversePosts = new List<Post>();           
			for (int i = posts.Count - 1; i >= 0; i--)
			{
				reversePosts.Add(posts[i]);
			}
            List<PostLikeComment> postLikeComments = new List<PostLikeComment>();

           

			foreach (var post in reversePosts)
            {
				List<Comment> comments = UnitOfWork.CommentRepository.GetCommentsByPostId(post.Id);
				List<Comment> reverseComments = new List<Comment>();
				for (int i = comments.Count - 1; i >= 0; i--)
				{
					reverseComments.Add(comments[i]);
				}
				List<UserComment> userComments = new List<UserComment>();
				foreach (var c in reverseComments)
				{
					User u = UnitOfWork.UserRepository.GetUser(c.UserId);
					userComments.Add(new UserComment
					{
						User = u,
						Comment = c
					});
				}
				postLikeComments.Add(new PostLikeComment
                {
                    Post = post,
                    CountLike = UnitOfWork.LikeRepository.CountLike(post.Id),
                    isLike = UnitOfWork.LikeRepository.isLike(user.Id, post.Id),
					UserComments = userComments
				});
            }

            List<Friend> listfriend = UnitOfWork.FriendRepository.GetFriend(user.Id);
            List<User> friends = new List<User>();
            foreach(var f in listfriend)
            {
                User u = UnitOfWork.UserRepository.GetUser(f.UserId2);
                friends.Add(u);
            }
			
			
			UserIndex userIndex = new UserIndex
			{
				User = user,
				CurrentUser = yourself,
				PostLikeComments = postLikeComments,
				CountFriends = UnitOfWork.FriendRepository.CountFriends(user.Id),
                Friends = friends
			};

			return View(userIndex);
		}
        public IActionResult EditName()
        {
            return RedirectToAction("Index");
        }
        public IActionResult DeletePost()
        {
            return RedirectToAction("Index");
        }
        public IActionResult GetUserImage(int id)
        {
            User user = UnitOfWork.UserRepository.GetUser(id);

            if (user?.Image != null)
            {
                return File(user.Image, "image/jpeg"); // Hoặc "image/png" tùy theo loại hình ảnh
            }
            var defaultImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user-image.jpg");

            var defaultImage = System.IO.File.ReadAllBytes(defaultImgPath);

            return File(defaultImage, "image/jpeg");
        }
        public IActionResult GetPostImage(int id)
        {
            var post = UnitOfWork.PostRepository.GetPostById(id);

            if (post?.Image != null)
            {
                return File(post.Image, "image/jpeg"); // Hoặc "image/png" tùy theo loại hình ảnh
            }


            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditNewImage(IFormFile ImagePersonal)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            var currentUser = UnitOfWork.UserRepository.GetUser(username);
			if (ImagePersonal != null && ImagePersonal.Length > 0)
			{

                // Đọc file vào bộ nhớ đệm
                using (var memoryStream = new MemoryStream())
                {
                    await ImagePersonal.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    UnitOfWork.UserRepository.EditNewImage(currentUser.Id, imageBytes);
                }
			}
            return RedirectToAction("Index");
		}
		[HttpPost]
        public async Task<IActionResult> CreatePost(string Content, IFormFile Image)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            var user = UnitOfWork.UserRepository.GetUser(username);
            if (Image != null && Image.Length > 0)
            {

                // Đọc file vào bộ nhớ đệm
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    // Lưu imageBytes vào cơ sở dữ liệu cùng với thông tin người dùng hoặc vào một thực thể lưu trữ khác
                    var post = new Post
                    {
                        UserId = user.Id,
                        Content = Content,
                        Image = imageBytes,
                        CreateDate = DateTime.Now
                    };

                    UnitOfWork.PostRepository.Create(post);
                }
            }
            else
            {
                if (Content == null)
                    return RedirectToAction("Index");

                var post = new Post
                {
                    UserId = user.Id,
                    Content = Content,
                    CreateDate = DateTime.Now
                };
                UnitOfWork.PostRepository.Create(post);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditName(string newName)
        {
            if(newName == null || newName == string.Empty)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var stringid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                UnitOfWork.UserRepository.EditNewName(int.Parse(stringid), newName);
                return RedirectToAction("Index");
            }            
        }
        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            UnitOfWork.PostRepository.DeletePost(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult LikePost(int postid)
        {
			var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            UnitOfWork.LikeRepository.LikePost(userid, postid);
            int countlike = UnitOfWork.LikeRepository.CountLike(postid);
            return Json(new { countlike = countlike});
        }
        [HttpPost]
        public IActionResult UnLikePost(int postid)
        {
			var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			UnitOfWork.LikeRepository.UnLikePost(userid, postid);
			int countlike = UnitOfWork.LikeRepository.CountLike(postid);

			return Json(new { countlike = countlike});
		}
        [HttpPost]
        public IActionResult CreateComment(int postId, string content)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
			var currentUser = UnitOfWork.UserRepository.GetUser(username);
			Comment comment = new Comment
            {
                UserId = currentUser.Id,
                PostId = postId,
                Content = content,
                Date = DateTime.Now
            };
            UnitOfWork.CommentRepository.CreateComment(comment);
            return Ok();
        }
        [HttpPost]
		public IActionResult LoadComments(int postid)
		{
			var comments = UnitOfWork.CommentRepository.GetCommentsByPostId(postid);
            List<Comment> reverseComments = new List<Comment>();
            for(int i = comments.Count - 1; i >= 0; i--)
            {
                reverseComments.Add(comments[i]);
            }

			List<UserComment> userComments = new List<UserComment>();
			foreach (var c in reverseComments)
			{
				User u = UnitOfWork.UserRepository.GetUser(c.UserId);
				userComments.Add(new UserComment
				{
					User = u,
					Comment = c
				});
			}
			return PartialView("_CommentsPartial", userComments);
		}
	}
}
