namespace Kebab_Simulator.Models.KebabModels
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
    public class KebabIndexViewModel
    {
        public Guid ID { get; set; }

        public string KebabName { get; set; }
        public int KebabXP { get; set; }
        public int KebabXPNextLevel { get; set; }
        public int KebabLevel { get; set; }
        public KebabType KebabType { get; set; }
        public int Checkout { get; set; }
        public int KebabBankAccount { get; set; }
        public DateTime KebabStart { get; set; }
        public DateTime KebabDone { get; set; }
        public KebabStatus KebabStatus { get; set; }
        //pp

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
