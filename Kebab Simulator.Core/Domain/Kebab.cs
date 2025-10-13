using Kebab_Simulator.Core.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain
{
    public enum KebabType
    {
        Shawarma, Döner, Falafel
    }
    public enum KebabStatus
    {
        Raw,
        Cooking,
        Cooked,
        ReadyToServe,
        Sold
    }
    public class Kebab
    {
        public Guid ID { get; set; }

        public string KebabName { get; set; }
        public int KebabXP { get; set; }
        public int KebabXPNextLevel { get; set; }
        public int KebabLevel { get; set; }
        public KebabType KebabType { get; set; }
        public int Checkout { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
        public int KebabBankAccount { get; set; }
        public DateTime KebabStart { get; set; }
        public DateTime KebabDone { get; set; }
        public KebabStatus KebabStatus { get; set; }

        //db only

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
