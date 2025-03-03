namespace Kebab_Simulator.Models.KebabBodies
{
	public class CountryImageViewModel
	{
		public Guid ImageID { get; set; }
		public string ImageTitle { get; set; }
		public byte[] ImageData { get; set; }
		public string Image { get; set; }
		public Guid? CountryID { get; set; }
	}
}
