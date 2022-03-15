using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;

namespace MasivRoulette.Services.Interfaces
{
    public interface IOpeningService
    {
        ResponseServiceModel OpenOpening(Opening roulette);
        ResponseServiceModel GetOpening(long idOpening);
        ResponseServiceModel CloseOpening(Opening idOpening);
    }
}
