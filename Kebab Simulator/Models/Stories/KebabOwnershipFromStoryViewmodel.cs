using Kebab_Simulator.Core.Domain.Dto;

namespace Kebab_Simulator.Models.Stories
{
    public class KebabOwnershipFromStoryViewmodel
    {
        public string PlayerProfileGuid { get; set; }
        public string StoryGUID { get; set; }
        public KebabOwnershipDto AddedKebab {  get; set; }
    }
}
