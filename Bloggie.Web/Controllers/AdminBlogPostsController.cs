using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminBlogPostsController : Controller
	{
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
		public async Task<IActionResult> Add()
		{
			// Get tags from repository -> we will display it in drop-down list
			var tags = await _tagRepository.GetAllAsync();

			var model = new AddBlogPostRequest()
			{
				// Text -> the label user sees in the dropdown, Value -> actual data that gets submitted when the form is posted
				// For each tag, create a SelectListItem to display in the drop-fown list
				Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
			};


			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
		{
			BlogPost blogPost = new BlogPost()
			{
				Heading = addBlogPostRequest.Heading,
				PageTitle = addBlogPostRequest.PageTitle,
				Content = addBlogPostRequest.Content,
				ShortDescription = addBlogPostRequest.ShortDescription,
				FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
				UrlHandle = addBlogPostRequest.UrlHandle,
				PublishedDate = addBlogPostRequest.PublishedDate,
				Author = addBlogPostRequest.Author,
				Visible = addBlogPostRequest.Visible,
            };

			var selectedTags = new List<Tag>();
			// Map Tags from selected tags
			foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
			{
				var tag = await _tagRepository.GetSingleAsync(Guid.Parse(selectedTagId));

				if (tag != null)
				{
                    selectedTags.Add(tag);
                }
		
            }

			blogPost.Tags = selectedTags;

			await _blogPostRepository.AddAsync(blogPost);


			return RedirectToAction("Add");
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var blogPost = await _blogPostRepository.GetAllAsync();

			return View(blogPost);
		}

		[HttpGet]
		// id comes from the asp-route-id -> has to match
		public async Task<IActionResult> Edit(Guid id)
		{
			var blogPost = await _blogPostRepository.GetSingleAsync(id);
			var tagsDomainModel = await _tagRepository.GetAllAsync();

			if (blogPost != null) 
			{
				EditBlogPostRequest model = new EditBlogPostRequest()
				{
					Id = blogPost.Id,
					Heading = blogPost.Heading,
					PageTitle = blogPost.PageTitle,
					Content = blogPost.Content,
					ShortDescription = blogPost.ShortDescription,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					UrlHandle = blogPost.UrlHandle,
					PublishedDate = blogPost.PublishedDate,
					Author = blogPost.Author,
					Visible = blogPost.Visible,
					Tags = tagsDomainModel.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}),
					SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),
				};

				return View(model);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
		{
			var blogPost = new BlogPost()
			{
				Id = editBlogPostRequest.Id,
				Heading = editBlogPostRequest.Heading,
				PageTitle = editBlogPostRequest.PageTitle,
				Content = editBlogPostRequest.Content,
				ShortDescription = editBlogPostRequest.ShortDescription,
				FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
				UrlHandle = editBlogPostRequest.UrlHandle,
				PublishedDate = editBlogPostRequest.PublishedDate,
				Author = editBlogPostRequest.Author,
				Visible = editBlogPostRequest.Visible,
			};

			var selectedTags = new List<Tag>();

			foreach (var selectedTag in editBlogPostRequest.SelectedTags)
			{
				if (Guid.TryParse(selectedTag, out var tag))
				{
					var foundTag = await _tagRepository.GetSingleAsync(tag);

					if (foundTag != null)
					{
						selectedTags.Add(foundTag);
					}
				}
			}

			blogPost.Tags = selectedTags;

			var updatedBlog = await _blogPostRepository.UpdateAsync(blogPost);

			if (updatedBlog != null)
			{
				// Show success notification
				return RedirectToAction("Edit");
			}

			// Show error notification
			return RedirectToAction("Edit");

		}

		[HttpPost]
		public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
		{
			 var deletedBlogPost = await _blogPostRepository.DeleteAsync(editBlogPostRequest.Id);

			if (deletedBlogPost != null)
			{
				// Show success notification
				return RedirectToAction("List");
			}

			// Show error notification
			return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
		}
	}
}
