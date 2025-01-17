using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain.Dto
{
    public class GalaxyDto
    {
        public Guid ID { get; set; }
        public string GalaxyName { get; set; }
        public string GalaxyLore { get; set; }
        public List<Guid> SolarSystemsInGalaxy { get; set; }
        public List<SolarSystem>? SolarSystemsInGalaxyObject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
