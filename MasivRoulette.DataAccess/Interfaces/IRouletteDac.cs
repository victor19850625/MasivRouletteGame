using MasivRoulette.Entities.Models;
using System.Data;

namespace MasivRoulette.DataAccess.Interfaces
{
    public  interface IRouletteDac
    {
        bool CreateRoulette(Roulette roulette);
        Roulette GetRoulette(long idRoulette);
        Roulette ModifyRoulette(Roulette roulette);
        DataSet GetAllRoulettes(out int countRoulettes);
    }
}
