using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasivRoulette.Services.Interfaces
{
    public interface IBetService
    {
        ResponseServiceModel CreateBet(Bet bet);
        ResponseServiceModel GetBet(long idBet);
        ResponseServiceModel GetBetIdOpening(long idOpening);
        ResponseServiceModel GetWinnersBet(Bet idOpening);
    }
}
