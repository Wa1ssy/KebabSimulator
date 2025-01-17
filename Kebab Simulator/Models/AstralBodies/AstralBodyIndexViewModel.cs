using Kebab_Simulator.Core.Domain;
using Kebab_Simulator.Models.KebabModels;
using KebabType = Kebab_Simulator.Models.KebabModels.KebabType;

namespace Kebab_Simulator.Models.AstralBodies
{
    public class AstralBodyIndexViewModel
    {
        public Guid ID { get; set; }
        public string AstralBodyName { get; set; }
        public AstralBodyType AstralBodyType { get; set; }
        public KebabType EnvironmentBoost { get; set; }
        public int MajorSettlements { get; set; }
        public KardashevScale TechnicalLevel { get; set; }
        //public Guid PlayerProfileID { get; set; }
        public string? SolarSystemID { get; set; }
        public List<AstralBodyImageViewModel>? Image { get; set; } = new List<AstralBodyImageViewModel>();

        //db only
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
