using MasivRoulette.Entities.Models;

namespace MasivRoulette.DataAccess.Interfaces
{
    public interface IOpeningDac
    {
        bool OpenOpening(Opening opening);
        Opening GetOpening(long idOpening);
        //Opening OpenOpening(long idOpening);
        Opening CloseOpening(Opening opening);
    }
}
