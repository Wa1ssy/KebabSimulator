namespace Kebab_Simulator.Models.ViewModels
{
    public enum KebabType
    {
        ErzanLegacy,
        AslansAmbition,
        SteppeFlame,
        NomadsFeast,
        GoldenYurt,
        SilkRoadSkewer,
        FathersPride,
        EaglesBite,
        KazakhSun,
        AslansRoar,
        SteppeWhisper,
        TravelersDelight,
        MountainSmoke,
        GoldenHorde,
        MidnightCaravan,
        EternalFlame,
        AslansTriumph,
        SteppeBreeze,
        LegendsSkewer,
        HeartOfTheStand
    }

    public enum KebabStatus
    {
        Raw,
        Cooking,
        Cooked,
        ReadyToServe,
        Sold
    }

    public class KebabViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public KebabType Type { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public KebabStatus Status { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }

        public decimal Price { get; set; }
    }
}
