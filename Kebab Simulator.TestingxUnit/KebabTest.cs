using System.Formats.Asn1;
using Kebab_Simulator.Core.Domain.Dto;

namespace Kebab_Simulator.TestingxUnit
{
    public class KebabTest
    {
        [Fact]
        public async Task ShouldNot_UpgradeWithoutMoney()
        {
            //ülesseade
            KebabDto testDto = new()
            {
                KebabType = (KebabType)Core.Domain.KebabType.ErzanLegacy,
                KebabXP = 50,
            };
        }
    }
}