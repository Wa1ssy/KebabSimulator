using System;

namespace Kebab_Simulator.Core.Services
{
    public static class HealthInspectionService
    {
        private static readonly Random _random = new Random();
        public static bool ShouldShowHealthInspection(double chance = 10.0)
        {
            return _random.NextDouble() < chance;
        }
    }
}
