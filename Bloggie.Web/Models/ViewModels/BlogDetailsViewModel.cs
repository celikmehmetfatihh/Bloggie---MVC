using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Models.ViewModels
{
	public class BlogDetailsViewModel
	{
		public Guid Id { get; set; }

		public string Heading { get; set; }

		public string PageTitle { get; set; }

		public string Content { get; set; }

		public string ShortDescription { get; set; }

		public string FeaturedImageUrl { get; set; }

		public string UrlHandle { get; set; }

		public DateTime PublishedDate { get; set; }

		public string Author { get; set; }

		public bool Visible { get; set; }

		public ICollection<Tag> Tags { get; set; }

		// Additional Property to display in the view
        public int TotalLikes { get; set; }

        public bool IsLiked { get; set; }

        public string CommentDescription { get; set; }

        public IEnumerable<BlogComment> Comments { get; set; }

    }
}
