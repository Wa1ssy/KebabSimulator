using Kebab_Simulator.Core.Domain;

namespace Kebab_Simulator.Models.KebabBodies
{
	public class CountryDetailsViewModel
	{
		public Guid ID { get; set; }
		public string Country { get; set; }
		public CountryType CountryType { get; set; }
		public List<Guid> RestaurantIDs { get; set; } = new List<Guid>();
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public List<RestaurantIndexViewModel>? Restaurants { get; set; } = new();
	}
}
