using System.Diagnostics;
using Bloggie.Web.Models;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
		private readonly ITagRepository _tagRepository;

		public HomeController(ILogger<HomeController> logger,
			IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
		{
			_logger = logger;
            _blogPostRepository = blogPostRepository;
			_tagRepository = tagRepository;
		}

		public async Task<IActionResult> Index()
		{
			var blogPosts = await _blogPostRepository.GetAllAsync();

			var tags = await _tagRepository.GetAllAsync();

			// Since here i will return two models, i created a new ViewModel that stores list of blogPosts and tags

			var model = new HomeViewModel()
			{
				BlogPosts = blogPosts,
				Tags = tags
			};

			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
