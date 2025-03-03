using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain.Dto
{
	public class RestaurantDto
	{
		public Guid ID { get; set; }
		public string RestaurantName { get; set; }
		public RestaurantType RestaurantType { get; set; }
		public Guid? CountryID { get; set; }
		public List<IFormFile> Files { get; set; }
		public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();


		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
