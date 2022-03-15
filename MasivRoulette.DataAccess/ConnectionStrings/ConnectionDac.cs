using MasivRoulette.Entities.BindingModelsConfiguration;
using Microsoft.Extensions.Options;

namespace MasivRoulette.DataAccess.ConnectionStrings.Interfaces
{
    public class ConnectionDac : IConnectionDac
    {
        private readonly ConnectionStringsBindingModel ConnectionStrings;
        public ConnectionDac(IOptions<ConnectionStringsBindingModel> connectionStrings)
        {
            this.ConnectionStrings = connectionStrings.Value;
        }
        public string GetConnection(bool isLocal)
        {
            if (isLocal)
                return this.ConnectionStrings.ConnectionBDLocal;
            else
                return this.ConnectionStrings.ConnectionBDCloud;
        }
    }
}
