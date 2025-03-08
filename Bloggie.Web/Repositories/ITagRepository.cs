using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Repositories
{
	public interface ITagRepository
	{
		Task<IEnumerable<Tag>> GetAllAsync();

		Task<Tag?> GetSingleAsync(Guid id);

		Task<Tag> AddAsync(Tag tag);

		Task<Tag?> UpdateAsync(Tag tag);

		Task<Tag?> DeleteAsync(Guid id);
	}
}
