using KebabType = Kebab_Simulator.Models.KebabModels.KebabType;
using Kebab_Simulator.Core.Domain;


namespace Kebab_Simulator.Models.AstralBodies
{
    public class AstralBodyCreateUpdateViewModel
    {
        public Guid? ID { get; set; }
        public string AstralBodyName { get; set; }
        public AstralBodyType AstralBodyType { get; set; }
        public KebabType EnvironmentBoost { get; set; }
        public string AstralBodyDescription { get; set; }
        public int MajorSettlements { get; set; }
        public KardashevScale TechnicalLevel { get; set; }
        //public List<Titan> TitansWhoOwnThisPlanet { get; set; }
        public Kebab? KebabWhoOwnsThisPlanet { get; set; }
        //public Guid PlayerProfileID { get; set; }
        public string? SolarSystemID { get; set; }

        //image
        public List<IFormFile> Files { get; set; }
        public List<AstralBodyImageViewModel> Image { get; set; } = new List<AstralBodyImageViewModel>();

        //db only
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
