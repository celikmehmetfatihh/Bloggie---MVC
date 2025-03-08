﻿using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
	public class TagRepository : ITagRepository
	{
		private readonly BloggieDbContext _bloggieDbContext;

		public TagRepository(BloggieDbContext bloggieDbContext)
        {
			_bloggieDbContext = bloggieDbContext;
		}

        public async Task<Tag> AddAsync(Tag tag)
		{
			await _bloggieDbContext.Tags.AddAsync(tag);
			await _bloggieDbContext.SaveChangesAsync();

			return tag;
		}

		public async Task<Tag?> DeleteAsync(Guid id)
		{
			var tag = await _bloggieDbContext.Tags.FindAsync(id);

			if (tag != null)
			{
				_bloggieDbContext.Tags.Remove(tag);
				await _bloggieDbContext.SaveChangesAsync();

				return tag;
			}

			return null;
		}

		public async Task<IEnumerable<Tag>> GetAllAsync()
		{
			var tags = await _bloggieDbContext.Tags.ToListAsync();

			return tags;
		}

		public async Task<Tag?> GetSingleAsync(Guid id)
		{
			var existingTag = await _bloggieDbContext.Tags.FindAsync(id);

			return existingTag;
		}

		public async Task<Tag?> UpdateAsync(Tag tag)
		{
			var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);

			if (existingTag != null)
			{
				existingTag.Name = tag.Name;
				existingTag.DisplayName = tag.DisplayName;

				await _bloggieDbContext.SaveChangesAsync();

				return existingTag;
			}

			return null;
		}
	}
}
