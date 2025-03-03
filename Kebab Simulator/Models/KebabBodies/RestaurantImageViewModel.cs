namespace Kebab_Simulator.Models.KebabBodies
{
	public class RestaurantImageViewModel
	{
		public Guid ImageID { get; set; }
		public string ImageTitle { get; set; }
		public byte[] ImageData { get; set; }
		public string Image { get; set; }
		public Guid? RestaurantID { get; set; }
	}
}
