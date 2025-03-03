using Kebab_Simulator.Core.Domain;

namespace Kebab_Simulator.Models.KebabBodies
{
	public class CountryIndexViewModel
	{
		public Guid ID { get; set; }
		public string Name { get; set; }
		public CountryType CountryType { get; set; }
		public List<CountryImageViewModel> Image { get; set; } = new List<CountryImageViewModel>();
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
