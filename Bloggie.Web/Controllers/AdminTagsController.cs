using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
	// Since SuperAdmin has already Admin as role, writing Admin has enough
    // Every method inside this controller needs to go through the authorization process
	[Authorize(Roles = "Admin")]
	public class AdminTagsController : Controller
    {
		private readonly ITagRepository _tagRepository;

		public AdminTagsController(ITagRepository tagRepository)
        {
			_tagRepository = tagRepository;
		}

        // https://localhost:7138/AdminTags/Add
         
        [HttpGet] // Display the Add Tag Form
        public IActionResult Add()
        {
            // If we dont specify a name for a view, it automatically by default looks at the name of action method in Views
            return View();
        }

        [HttpPost] // Submit the Add Tag Form
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await _tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var tags = await _tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        // name of the parameter has to match asp-route-id 
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagRepository.GetSingleAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest()
                {
                    Id = id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

				return View(editTagRequest);
			}

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag()
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };


            var updatedTag = await _tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                // Show success notification
            }
            else
            {
				// Show error notification

			}
			return RedirectToAction("Edit", new {id = editTagRequest.Id});

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await _tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null) 
            {
                // Show success notification
                return RedirectToAction("List");
			}
			else
            {
				// Show error notification
				return RedirectToAction("Edit", new { id = editTagRequest.Id });
			}
		}
    }
}
