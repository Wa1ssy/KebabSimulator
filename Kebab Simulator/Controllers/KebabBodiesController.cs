using Kebab_Simulator.ApplicationServices.Services;
using Kebab_Simulator.Core.Domain.Dto;
using Kebab_Simulator.Core.Domain.Serviceinterface;
using Kebab_Simulator.Data;
using Kebab_Simulator.Models.KebabBodies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kebab_Simulator.Controllers
{
	public class KebabBodiesController : Controller
	{
		private readonly KebabSimulatorContext _context;
		private readonly ICountriesServices _countriesServices;
		private readonly IFileServices _fileServices;

		public KebabBodiesController(
			KebabSimulatorContext context,
			ICountriesServices countriesServices,
			IFileServices fileServices)
		{
			_context = context;
			_countriesServices = countriesServices;
			_fileServices = fileServices;
		}

		public IActionResult Index()
		{
			var countries = _context.Countries
				.OrderByDescending(c => c.CreatedAt)
				.Select(c => new CountryIndexViewModel
				{
					ID = c.ID,
					Name = c.Name,
					CountryType = (Core.Domain.CountryType)c.CountryType
				});

			return View(countries);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View(new CountryCreateViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(CountryCreateViewModel vm)
		{
			var dto = new CountryDto
			{
				Name = vm.Name,
				CountryType = (Core.Domain.CountryType)vm.CountryType,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				Files = vm.Files,
				Image = vm.Image.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					CountryID = x.CountryID
				}).ToArray()
			};

			var result = await _countriesServices.Create(dto);

			if (result != null)
			{
				return RedirectToAction("Index");
			}

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var country = await _countriesServices.DetailsAsync(id);
			if (country == null) return NotFound();

			var images = await _context.FilesToDatabase
				.Where(f => f.CountryID == id)
				.Select(f => new CountryImageViewModel
				{
					CountryID = f.CountryID,
					ImageID = f.ID,
					ImageData = f.ImageData,
					ImageTitle = f.ImageTitle,
					Image = $"data:image/gif;base64,{Convert.ToBase64String(f.ImageData)}"
				}).ToArrayAsync();

			var vm = new CountryDetailsViewModel
			{
				ID = country.ID,
				Name = country.Name,
				CountryType = (Core.Domain.CountryType)country.CountryType,
				CreatedAt = country.CreatedAt,
				UpdatedAt = country.UpdatedAt
			};

			vm.Image.AddRange(images);
			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			var country = await _countriesServices.DetailsAsync(id);
			if (country == null) return NotFound();

			var images = await _context.FilesToDatabase
				.Where(f => f.CountryID == id)
				.Select(f => new CountryImageViewModel
				{
					CountryID = f.CountryID,
					ImageID = f.ID,
					ImageData = f.ImageData,
					ImageTitle = f.ImageTitle,
					Image = $"data:image/gif;base64,{Convert.ToBase64String(f.ImageData)}"
				}).ToArrayAsync();

			var vm = new CountryCreateViewModel
			{
				ID = country.ID,
				Name = country.Name,
				CountryType = (Core.Domain.CountryType)country.CountryType,
				CreatedAt = country.CreatedAt,
				UpdatedAt = DateTime.Now
			};

			vm.Image.AddRange(images);
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Update(CountryCreateViewModel vm)
		{
			if (!ModelState.IsValid) return View(vm);

			var dto = new CountryDto
			{
				ID = vm.ID,
				Name = vm.Name,
				CountryType = (Core.Domain.CountryType)vm.CountryType,
				CreatedAt = vm.CreatedAt,
				UpdatedAt = DateTime.Now,
				Files = vm.Files,
				Image = vm.Image.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					CountryID = x.CountryID
				}).ToArray()
			};

			var result = await _countriesServices.Update(dto);

			if (result != null)
			{
				return RedirectToAction("Index");
			}

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var country = await _countriesServices.DetailsAsync(id);
			if (country == null) return NotFound();

			var images = await _context.FilesToDatabase
				.Where(f => f.CountryID == id)
				.Select(f => new CountryImageViewModel
				{
					CountryID = f.CountryID,
					ImageID = f.ID,
					ImageData = f.ImageData,
					ImageTitle = f.ImageTitle,
					Image = $"data:image/gif;base64,{Convert.ToBase64String(f.ImageData)}"
				}).ToArrayAsync();

			var vm = new CountryDeleteViewModel
			{
				ID = country.ID,
				Name = country.Name,
				CountryType = (Core.Domain.CountryType)country.CountryType,
				CreatedAt = country.CreatedAt
			};

			vm.Image.AddRange(images);
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var result = await _countriesServices.Delete(id);
			if (result == null) return NotFound();

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveImage(CountryImageViewModel vm)
		{
			var dto = new FileToDatabaseDto
			{
				ID = vm.ImageID
			};

			await _fileServices.RemoveImageFromDatabase(dto);
			return RedirectToAction("Index");
		}
	}
}