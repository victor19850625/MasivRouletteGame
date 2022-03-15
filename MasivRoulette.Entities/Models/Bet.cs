using System;
using System.Data;

namespace MasivRoulette.Entities.Models
{
    public class Bet
    {
        #region Constructor
        public Bet()
        {
        }
        public Bet(long idOpening, int numberBet, string colorBet)
        {
            IdOpening = idOpening;
            NumberBet = numberBet;
            ColorBet = colorBet;
        }
        public Bet(DataRow opening_dr)
        {
            this.IdBet = Convert.ToInt64(opening_dr["IdBet"]);
            this.RegisterBet = Convert.ToDateTime(opening_dr["RegisterBet"]);
            this.IdOpening = Convert.ToInt64(opening_dr["IdOpening"]);
            this.IdUser = Convert.ToInt64(opening_dr["IdUser"]);
            this.NumberBet = Convert.ToInt16(opening_dr["NumberBet"]);
            this.ColorBet = Convert.ToString(opening_dr["ColorBet"]);
            this.ValueBet = Convert.ToDecimal(opening_dr["ValueBet"]);
        }
        #endregion Constructor

        #region Properties
        public long IdBet { get; set; }
        public DateTime RegisterBet { get; set; }
        public long IdOpening { get; set; }
        public long IdUser { get; set; }
        public int? NumberBet { get; set; }
        public string ColorBet { get; set; }
        public decimal ValueBet { get; set; }
        #endregion Properties
    }
}
