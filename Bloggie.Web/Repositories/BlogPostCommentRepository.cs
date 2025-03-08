using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
	public class BlogPostCommentRepository : IBlogPostCommentRepository
	{
		private readonly BloggieDbContext _dbContext;

		public BlogPostCommentRepository(BloggieDbContext dbContext)
        {
			_dbContext = dbContext;
		}

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
		{
			await _dbContext.BlogPostComment.AddAsync(blogPostComment);
			await _dbContext.SaveChangesAsync();
			return blogPostComment;
		}

		public async Task<IEnumerable<BlogPostComment>> GetAllByBlogIdAsync(Guid blogPostId)
		{
			return await _dbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
		}
	}
}
