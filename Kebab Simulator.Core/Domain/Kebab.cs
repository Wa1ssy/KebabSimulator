using Kebab_Simulator.Core.Domain.Dto;
using System;
using System.Collections.Generic;

namespace Kebab_Simulator.Core.Domain
{


    public class Kebab
    {
        public Guid ID { get; set; }
        public string KebabName { get; set; }
        public int KebabXP { get; set; }
        public int KebabXPNextLevel { get; set; }
        public int KebabLevel { get; set; }
        public int Checkout { get; set; }
        public int Ingredients { get; set; }
        public DateTime? LastAssistantPayedAt { get; set; }
        public int Reputation { get; set; }
        public int CookSpeed { get; set; }
        public int GrillLevel { get; set; }     // 0–5
        public int SpiceLevel { get; set; }     // 0–5
        public int AssistantLevel { get; set; } // 0–3

        public int PassiveIncomeLevel { get; set; } 
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
        public int KebabBankAccount { get; set; }
        public DateTime KebabStart { get; set; }
        public DateTime KebabDone { get; set; }
        public KebabStatus KebabStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public KebabType KebabType { get; set; }
    }
}
