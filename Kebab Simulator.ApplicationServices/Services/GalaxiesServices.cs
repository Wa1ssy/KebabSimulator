using Kebab_Simulator.Core.Domain.Dto;
using Kebab_Simulator.Core.Domain.Serviceinterface;
using Kebab_Simulator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kebab_Simulator.Data;
using Microsoft.EntityFrameworkCore;

namespace Kebab_Simulator.ApplicationServices.Services
{
	public class GalaxiesServices : IGalaxiesServices
	{
		private readonly KebabSimulatorContext _context;
		private readonly IFileServices _fileServices;
		private readonly IAstralBodiesServices _astralBodiesServices;
		private readonly ISolarSystemServices _solarSystemsServices;

		public GalaxiesServices(
			KebabSimulatorContext context,
			IFileServices fileServices,
			IAstralBodiesServices astralBodiesServices,
			ISolarSystemServices solarSystemsServices)
		{
			_context = context;
			_fileServices = fileServices;
			_astralBodiesServices = astralBodiesServices;
			_solarSystemsServices = solarSystemsServices;
		}

		public async Task<Galaxy> DetailsAsync(Guid id)
		{
			var result = await _context.Galaxies
				.FirstOrDefaultAsync(x => x.ID == id);
			return result;
		}

		public async Task<Galaxy> Create(GalaxyDto dto, List<SolarSystem> systemsInGalaxy)
		{
			Galaxy newGalaxy = new();

			newGalaxy.ID = Guid.NewGuid();


			newGalaxy.GalaxyName = dto.GalaxyName;
			newGalaxy.GalaxyLore = dto.GalaxyLore;
			newGalaxy.SolarSystemsInGalaxy = SystemToID(systemsInGalaxy);

			newGalaxy.CreatedAt = DateTime.Now;
			newGalaxy.UpdatedAt = DateTime.Now;
			await _context.Galaxies.AddAsync(newGalaxy);
			await _context.SaveChangesAsync();

			return newGalaxy;
		}

		public async Task<Galaxy> Update(GalaxyDto dto, List<SolarSystem> systemsInGalaxy, List<SolarSystem> removedSystems)
		{
			Galaxy modifiedGalaxy = new();

			modifiedGalaxy.ID = (Guid)dto.ID;

			modifiedGalaxy.GalaxyName = dto.GalaxyName;
			modifiedGalaxy.GalaxyLore = dto.GalaxyLore;
			modifiedGalaxy.SolarSystemsInGalaxy = dto.SolarSystemsInGalaxy;
			modifiedGalaxy.CreatedAt = DateTime.Now;
			modifiedGalaxy.UpdatedAt = DateTime.Now;
			_context.Galaxies.Update(modifiedGalaxy);
			await _context.SaveChangesAsync();

			return modifiedGalaxy;
		}

		public async Task<Galaxy> Delete(Galaxy galaxyToBeDeleted)
		{
			var result = await _context.Galaxies.FirstOrDefaultAsync(x => x.ID == galaxyToBeDeleted.ID);
			_context.Galaxies.Remove(result);
			await _context.SaveChangesAsync();

			return result;
		}

		private static List<Guid> SystemToID(List<SolarSystem> systems)
		{
			var result = new List<Guid>();
			foreach (var system in systems)
			{
				result.Add(system.ID);
			}
			return result;
		}
	}
}
