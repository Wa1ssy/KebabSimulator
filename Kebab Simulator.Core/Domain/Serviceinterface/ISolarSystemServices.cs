using Kebab_Simulator.Core.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain.Serviceinterface
{
    public interface ISolarSystemServices
    {
        Task<SolarSystem> DetailsAsync(Guid id);
        Task<SolarSystem> Create(SolarSystemDto dto, List<AstralBody> planetsInSystem);
        Task<SolarSystem> Update(SolarSystemDto dto, List<AstralBody> planetsInSystem, List<AstralBody> removedPlanets);
        Task<SolarSystem> Delete(List<AstralBody> body, SolarSystem system, List<Guid> planetIDs);
    }
}
