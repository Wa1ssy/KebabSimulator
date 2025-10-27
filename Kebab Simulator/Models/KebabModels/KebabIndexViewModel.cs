namespace Kebab_Simulator.Models.KebabModels
{
    public class KebabIndexViewModel
    {
        public Guid ID { get; set; }

        public string KebabName { get; set; }
        public int KebabXP { get; set; }
        public int KebabXPNextLevel { get; set; }
        public int KebabLevel { get; set; }
        public int Checkout { get; set; }
        public int KebabBankAccount { get; set; }
        public DateTime KebabStart { get; set; }
        public DateTime KebabDone { get; set; }
        //pp

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
