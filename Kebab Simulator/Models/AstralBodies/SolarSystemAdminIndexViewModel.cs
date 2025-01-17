using Kebab_Simulator.Core.Domain;

namespace Kebab_Simulator.Models.AstralBodies
{
    public class SolarSystemAdminIndexViewModel
    {
        public Guid ID { get; set; }
        public Guid AstralBodyAtCenter { get; set; }
        public AstralBody? AstralBodyAtCenterWith { get; set; }
        public string SolarSystemName { get; set; }
        public string SolarSystemLore { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
