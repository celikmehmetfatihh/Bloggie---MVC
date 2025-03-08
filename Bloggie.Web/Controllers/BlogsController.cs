using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
		private readonly IBlogPostLikeRepository _blogPostLikeRepository;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IBlogPostCommentRepository _blogPostCommentRepository;

		public BlogsController(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager,
			IBlogPostCommentRepository blogPostCommentRepository)
        {
            _blogPostRepository = blogPostRepository;
			_blogPostLikeRepository = blogPostLikeRepository;
			_signInManager = signInManager;
			_userManager = userManager;
			_blogPostCommentRepository = blogPostCommentRepository;
		}

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlHandle);
			var blogDetailsViewModel = new BlogDetailsViewModel();
			// Is the user liked the current post
			var isLiked = false;

			if (blogPost != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikesAsync(blogPost.Id);

				if (_signInManager.IsSignedIn(User))
				{
					// Get like for the blog for this user
					var likesForBlog = await _blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

					var userId = _userManager.GetUserId(User);

					if (userId != null)
					{
						var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));

						if (likeFromUser != null)
						{
							isLiked = true;
						}
					}
				}

				// Get comments for blog post
				var blogCommentsDomainModel = await _blogPostCommentRepository.GetAllByBlogIdAsync(blogPost.Id);

				var blogCommentsForView = new List<BlogComment>();

				foreach (var blogComment in blogCommentsDomainModel)
				{
					blogCommentsForView.Add(new BlogComment
					{
						Description = blogComment.Description,
						DateAdded = blogComment.DateAdded,
						// Get Username from UserId using userManager
						Username = (await _userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
					});
				}


				blogDetailsViewModel = new BlogDetailsViewModel()
				{
					Id = blogPost.Id,
					Content = blogPost.Content,
					PageTitle = blogPost.PageTitle,
					Author = blogPost.Author,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					Heading = blogPost.Heading,
					PublishedDate = blogPost.PublishedDate,
					ShortDescription = blogPost.ShortDescription,
					UrlHandle = blogPost.UrlHandle,
					Visible = blogPost.Visible,
					Tags = blogPost.Tags,
					TotalLikes = totalLikes,
					IsLiked = isLiked,
					Comments = blogCommentsForView
				};

			}

			return View(blogDetailsViewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
		{
			var userId = "";

			if (_signInManager.IsSignedIn(User))
			{
				var blogPostCommentModel = new BlogPostComment()
				{
					Description = blogDetailsViewModel.CommentDescription,
					BlogPostId = blogDetailsViewModel.Id,
					UserId = Guid.Parse(_userManager.GetUserId(User)),
					DateAdded = DateTime.Now
				};


				await _blogPostCommentRepository.AddAsync(blogPostCommentModel);

				return RedirectToAction("Index", "Blogs", new
				{
					urlHandle = blogDetailsViewModel.UrlHandle
				});
			}

			return View();
		}
    }
}
