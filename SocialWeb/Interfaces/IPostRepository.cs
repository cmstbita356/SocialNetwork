using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SocialWeb.Models;

namespace SocialWeb.Interfaces
{
	public interface IPostRepository
	{
		bool Create(Post post);
		List<Post> GetAllByUserId(int userid);
		Post GetPostById(int id);
		bool DeletePost(int id);
	}
}
