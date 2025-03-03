using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kebab_Simulator.Core.Domain.Dto;

namespace Kebab_Simulator.Core.Domain.Serviceinterface
{
	public interface ICountriesServices
	{
		Task<Country> DetailsAsync(Guid id);
		Task<Country> Delete(Guid id);
		Task<Country> Update(CountryDto dto);
		Task<Country> Create(CountryDto dto);
	}
}
