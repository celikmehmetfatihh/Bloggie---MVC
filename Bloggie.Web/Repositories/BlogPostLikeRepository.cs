
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
	public class BlogPostLikeRepository : IBlogPostLikeRepository
	{
		private readonly BloggieDbContext _dbContext;

		public BlogPostLikeRepository(BloggieDbContext dbContext)
        {
			_dbContext = dbContext;
		}

		public async Task<BlogPostLike> AddLikeForBlogAsync(BlogPostLike blogPostLike)
		{
			await _dbContext.BlogPostLikes.AddAsync(blogPostLike);
			await _dbContext.SaveChangesAsync();

			return blogPostLike;
		}

		public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
		{
			return await _dbContext.BlogPostLikes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
		}

		public async Task<int> GetTotalLikesAsync(Guid blogPostId)
		{
			return await _dbContext.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
		}
	}
}
