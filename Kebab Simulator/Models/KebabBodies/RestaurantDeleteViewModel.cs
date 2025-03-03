using Kebab_Simulator.Core.Domain;

namespace Kebab_Simulator.Models.KebabBodies
{
	public class RestaurantDeleteViewModel
	{
		public Guid ID { get; set; }
		public string RestaurantName { get; set; }
		public RestaurantType RestaurantType { get; set; }
		public Guid? CountryID { get; set; }
		public List<RestaurantImageViewModel> Image { get; set; } = new List<RestaurantImageViewModel>();
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
