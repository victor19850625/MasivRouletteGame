using MasivRoulette.Entities.Models;
using System.Data;

namespace MasivRoulette.DataAccess.Interfaces
{    
    public interface IUserDac
    {
        bool CreateUser(User user);
        User GetUser(long idUser);
        User ModifyUser(User user);
    }
}
