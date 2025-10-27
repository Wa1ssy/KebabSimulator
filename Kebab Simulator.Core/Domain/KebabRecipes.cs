using System;

namespace Kebab_Simulator.Core.Domain
{

    public enum KebabStatus
    {
        Raw,
        Cooking,
        Cooked,
        ReadyToServe,
        Sold
    }
    public class KebabRecipe
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public KebabType KebabType { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public int LevelRequired { get; set; }
        public KebabStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public decimal Price { get; set; }
    }
}
