using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;

namespace MasivRoulette.Services.Interfaces
{
    public interface IRouletteService
    {
        ResponseServiceModel CreateRoulette(Roulette roulette);
        ResponseServiceModel GetRoulette(long idRoulette);
        ResponseServiceModel ModifyRoulette(Roulette roulette);
        ResponseServiceModel GetAllRoulettes();
    }
}
