using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;

namespace MasivRoulette.Services.Interfaces
{
    public interface IUserService
    {
        ResponseServiceModel CreateUser(User user);
        ResponseServiceModel GetUser(long idUser);
        ResponseServiceModel ModifyUser(User user);
    }
}
