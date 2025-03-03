using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain
{
	public enum RestaurantType
	{
		One,
		Two,
		Three,
		Four,
		Five,
	}
	public class Restaurant
	{
		public Guid ID { get; set; }
		public string RestaurantName { get; set; }
		public RestaurantType RestaurantType { get; set; }
		public Guid? CountryID { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
