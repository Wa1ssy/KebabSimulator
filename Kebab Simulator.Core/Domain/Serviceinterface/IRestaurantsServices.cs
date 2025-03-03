using Kebab_Simulator.Core.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain.Serviceinterface
{
	public interface IRestaurantsServices
	{
		Task<Restaurant> DetailsAsync(Guid id);
		Task<Restaurant> Delete(Guid id);
		Task<Restaurant> Update(RestaurantDto dto);
		Task<Restaurant> Create(RestaurantDto dto);
	}
}
