using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasivRoulette.DataAccess.ConnectionStrings.Interfaces
{
    public interface IConnectionDac
    {
        string GetConnection(bool isLocal);
    }
}
