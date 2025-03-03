using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain
{
	public enum CountryType
	{
		African,
		Asian,
		European,
		Indigenous,
		MiddleEastern,
		LatinAmerican,
	}
	public class Country
	{
		public Guid ID { get; set; }
		public string Name { get; set; }
		public CountryType CountryType { get; set; }
		public List<Guid> RestaurantIDs { get; set; } = new List<Guid>();
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
