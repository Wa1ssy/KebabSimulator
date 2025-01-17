using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain.Serviceinterface
{
    public interface IAstralBodiesServices
    {
        Task<AstralBody> DetailsAsync(Guid id);
        Task<AstralBody> Create(AstralBodyDto dto);
        Task<AstralBody> Delete(Guid id);
        Task<AstralBody> Update(AstralBodyDto dto);

    }
}
