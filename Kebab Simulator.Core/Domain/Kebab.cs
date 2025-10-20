using Kebab_Simulator.Core.Domain.Dto;
using System;
using System.Collections.Generic;

namespace Kebab_Simulator.Core.Domain
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

    public class Kebab
    {
        public Guid ID { get; set; }
        public string KebabName { get; set; }
        public int KebabXP { get; set; }
        public int KebabXPNextLevel { get; set; }
        public int KebabLevel { get; set; }
        public KebabType KebabType { get; set; }
        public int Checkout { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
        public int KebabBankAccount { get; set; }
        public DateTime KebabStart { get; set; }
        public DateTime KebabDone { get; set; }
        public KebabStatus KebabStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
