// CountriesServices implementation
using Kebab_Simulator.Core.Domain;
using Kebab_Simulator.Core.Domain.Dto;
using Kebab_Simulator.Core.Domain.Serviceinterface;
using Kebab_Simulator.Data;
using Microsoft.EntityFrameworkCore;

namespace Kebab_Simulator.ApplicationServices.Services
{
	public class CountriesServices : ICountriesServices
	{
		private readonly KebabSimulatorContext _context;

		public CountriesServices(KebabSimulatorContext context)
		{
			_context = context;
		}

		public async Task<Country> Create(CountryDto dto)
		{
			var country = new Country
			{
				ID = Guid.NewGuid(),
				Name = dto.Name,
				CountryType = dto.CountryType,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			};

			_context.Countries.Add(country);

			if (dto.Files != null)
			{
				foreach (var file in dto.Files)
				{
					using var ms = new MemoryStream();
					await file.CopyToAsync(ms);

					_context.FilesToDatabase.Add(new FileToDatabase
					{
						ID = Guid.NewGuid(),
						ImageData = ms.ToArray(),
						ImageTitle = file.FileName,
						CountryID = country.ID
					});
				}
			}

			await _context.SaveChangesAsync();
			return country;
		}

		public async Task<Country> DetailsAsync(Guid id)
		{
			return await _context.Countries
				.FirstOrDefaultAsync(c => c.ID == id);
		}

		public async Task<Country> Update(CountryDto dto)
		{
			var country = await _context.Countries.FindAsync(dto.ID);
			if (country == null) return null;

			country.Name = dto.Name;
			country.CountryType = dto.CountryType;
			country.UpdatedAt = DateTime.Now;

			// Handle image updates
			if (dto.Files != null)
			{
				foreach (var file in dto.Files)
				{
					using var ms = new MemoryStream();
					await file.CopyToAsync(ms);

					_context.FilesToDatabase.Add(new FileToDatabase
					{
						ID = Guid.NewGuid(),
						ImageData = ms.ToArray(),
						ImageTitle = file.FileName,
						CountryID = country.ID
					});
				}
			}

			await _context.SaveChangesAsync();
			return country;
		}

		public async Task<Country> Delete(Guid id)
		{
			var country = await _context.Countries.FindAsync(id);
			if (country == null) return null;

			_context.Countries.Remove(country);
			await _context.SaveChangesAsync();
			return country;
		}
	}
}