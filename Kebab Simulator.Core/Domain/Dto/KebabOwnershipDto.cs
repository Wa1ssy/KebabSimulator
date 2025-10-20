using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain.Dto
{
    public class KebabOwnershipDto : KebabDto
    {
        public Guid OwnerShipID { get; set; }

        public int KebabXP { get; set; }
        public int KebabXPNextLevel { get; set; }
        public int KebabLevel { get; set; }
        public int Checkout { get; set; }
        public int KebabBankAccount { get; set; }
        public DateTime KebabStart { get; set; }
        public DateTime KebabDone { get; set; }
        public KebabStatus KebabStatus { get; set; }


        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
        //db only


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
