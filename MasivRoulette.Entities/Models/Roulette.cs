using System;
using System.Data;

namespace MasivRoulette.Entities.Models
{
    public class Roulette
    {
        #region Constructor
        public Roulette()
        {

        }
        public Roulette(DataRow roulette_dr)
        {
            this.IdRoulette = Convert.ToInt64(roulette_dr["IdRoulette"]);
            this.RegisterRoulette = Convert.ToDateTime(roulette_dr["RegisterRoulette"]);
            this.TitleRoulette = Convert.ToString(roulette_dr["TitleRoulette"]);
            this.StateRoulette = Convert.ToBoolean(roulette_dr["StateRoulette"]);
        }
        #endregion Constructor

        #region Properties
        public long IdRoulette { get; set; }
        public DateTime RegisterRoulette { get; set; }
        public string TitleRoulette { get; set; }
        public bool? StateRoulette { get; set; }
        #endregion Properties
    }
}
