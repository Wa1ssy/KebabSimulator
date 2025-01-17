using Kebab_Simulator.Core.Domain;

namespace Kebab_Simulator.Models.AstralBodies
{
    public class GalaxyViewModel
    {
            public Guid? ID { get; set; }
            public string GalaxyName { get; set; }
            public string GalaxyLore { get; set; }
            public List<Guid>? SolarSystemsInGalaxy { get; set; }
            public List<SolarSystem>? SolarSystemsInGalaxyObject { get; set; }

            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
    }
}
