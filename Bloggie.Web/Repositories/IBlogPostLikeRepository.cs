using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
	public interface IBlogPostLikeRepository
	{
		Task<int> GetTotalLikesAsync(Guid blogPostId);

		Task<BlogPostLike> AddLikeForBlogAsync(BlogPostLike blogPostLike);

		Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
	}
}
