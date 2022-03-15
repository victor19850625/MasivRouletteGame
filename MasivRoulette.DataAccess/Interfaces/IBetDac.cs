using MasivRoulette.Entities.Models;
using System.Data;

namespace MasivRoulette.DataAccess.Interfaces
{
    public interface IBetDac
    {
        bool CreateBet(Bet bet);
        Bet GetBet(long idBet);
        DataSet GetBetsIdOpening(long idOpening);
        DataSet GetWinnersBet(Bet bet);
    }
}
