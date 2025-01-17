using Kebab_Simulator.Core.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain.Serviceinterface
{
	public interface IGalaxiesServices
    {
        Task<Galaxy> DetailsAsync(Guid id);
        Task<Galaxy> Create(GalaxyDto dto, List<SolarSystem> systemsInGalaxy);
        Task<Galaxy> Update(GalaxyDto dto, List<SolarSystem> systemsInGalaxy, List<SolarSystem> removedSystems);
        Task<Galaxy> Delete(Galaxy galaxyToBeDeleted);
    }
}
