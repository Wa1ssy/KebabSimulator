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
	public class AstralBodiesServices : IAstralBodiesServices
	{
		private readonly KebabSimulatorContext _context;
		private readonly IFileServices _fileServices;

		public AstralBodiesServices(KebabSimulatorContext context, IFileServices fileServices)
		{
			_context = context;
			_fileServices = fileServices;
		}
		public async Task<AstralBody> DetailsAsync(Guid id)
		{
			var result = await _context.AstralBodies
				.FirstOrDefaultAsync(x => x.ID == id);
			return result;
		}

		public async Task<AstralBody> Create(AstralBodyDto dto)
		{
			Random RNG = new Random();
			int settlementcount;
			if (dto.MajorSettlements == null)
			{ settlementcount = RNG.Next(1, 200); }
			else { settlementcount = dto.MajorSettlements; }

			AstralBody newPlanet = new();


			newPlanet.ID = Guid.NewGuid();
			newPlanet.MajorSettlements = settlementcount;
			if (settlementcount >= 189)
			{ newPlanet.TechnicalLevel = KardashevScale.Type3; }
			else if (190 >= settlementcount && settlementcount >= 150)
			{ newPlanet.TechnicalLevel = KardashevScale.Type2; }
			else { newPlanet.TechnicalLevel = KardashevScale.Type1; }

			newPlanet.AstralBodyName = dto.AstralBodyName;
			newPlanet.AstralBodyType = dto.AstralBodyType;
			newPlanet.EnvironmentBoost = (Core.Domain.KebabType)dto.EnvironmentBoost;
			newPlanet.AstralBodyDescription = dto.AstralBodyDescription;


			newPlanet.CreatedAt = DateTime.Now;
			newPlanet.ModifiedAt = DateTime.Now;

			if (dto.Files != null)
			{
				_fileServices.UploadFilesToDatabase(dto, newPlanet);
			}

			await _context.AstralBodies.AddAsync(newPlanet);
			await _context.SaveChangesAsync();
			return newPlanet;
		}

		public async Task<AstralBody> Update(AstralBodyDto dto)
		{
			AstralBody astralChangedBody = new();

			astralChangedBody.ID = dto.ID;
			astralChangedBody.AstralBodyName = dto.AstralBodyName;
			astralChangedBody.AstralBodyType = dto.AstralBodyType;
			astralChangedBody.EnvironmentBoost = (Core.Domain.KebabType)dto.EnvironmentBoost;
			astralChangedBody.AstralBodyDescription = dto.AstralBodyDescription;
			astralChangedBody.MajorSettlements = dto.MajorSettlements;
			astralChangedBody.TechnicalLevel = dto.TechnicalLevel;
			astralChangedBody.KebabWhoManagesPlanet = dto.KebabWhoManagesPlanet;
			astralChangedBody.SolarSystemID = dto.SolarSystemID;
			astralChangedBody.CreatedAt = dto.CreatedAt;
			astralChangedBody.ModifiedAt = DateTime.Now;

			if (dto.Files != null)
			{
				_fileServices.UploadFilesToDatabase(dto, astralChangedBody);
			}

			_context.AstralBodies.Update(astralChangedBody);
			await _context.SaveChangesAsync();
			return astralChangedBody;

		}

		public async Task<AstralBody> Delete(Guid id)
		{
			var result = await _context.AstralBodies.FirstOrDefaultAsync(x => x.ID == id);
			_context.AstralBodies.Remove(result);
			await _context.SaveChangesAsync();
			return result;
		}

	}
}
