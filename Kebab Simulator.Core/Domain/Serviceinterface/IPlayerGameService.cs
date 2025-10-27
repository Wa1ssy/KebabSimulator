using Kebab_Simulator.Models.ViewModels;

namespace Kebab_Simulator.Core.ServiceInterface
{
    public interface IPlayerGameService
    {
        PlayerViewModel GetPlayer();
        void CookKebab();
    }
}
