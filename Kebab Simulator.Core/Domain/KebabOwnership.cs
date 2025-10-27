using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.Domain
{
	public class KebabOwnership
	{
		[Key]
        public Guid OwnerShipID { get; set; }
		public int KebabXP { get; set; }
		public int KebabXPNextLevel { get; set; }
		public int KebabLevel { get; set; }
		public int Checkout { get; set; }
		public int KebabBankAccount { get; set; }
		public DateTime KebabStart { get; set; } 
		public DateTime KebabDone { get; set; }
		public KebabStatus KebabStatus { get; set; }
		public DateTime OwnershipCreatedAt { get; set; }
		public DateTime OwnershipUpdatedAt { get; set; }

	}
}
