using System;
using System.Data;

namespace MasivRoulette.Entities.Models
{
    public class Opening
    {
        #region Constructor
        public Opening()
        {
        }
        public Opening(DataRow opening_dr)
        {
            this.IdOpening = Convert.ToInt64(opening_dr["IdOpening"]);
            this.IdRoulette = Convert.ToInt64(opening_dr["IdRoulette"]);
            this.DateStartOpening = Convert.IsDBNull(opening_dr["DateStartOpening"]) ? null : Convert.ToDateTime(opening_dr["DateStartOpening"]);
            this.DateFinishOpening = Convert.IsDBNull(opening_dr["DateFinishOpening"]) ? null : Convert.ToDateTime(opening_dr["DateFinishOpening"]);
            this.NumberOpening = Convert.IsDBNull(opening_dr["NumberOpening"]) ? null : Convert.ToInt16(opening_dr["NumberOpening"]);
            this.ColorOpening = Convert.IsDBNull(opening_dr["ColorOpening"]) ? null : Convert.ToString(opening_dr["ColorOpening"]);
            this.EnableOpening = Convert.ToBoolean(opening_dr["EnableOpening"]);
        }
        #endregion Constructor

            #region Properties
        public long IdOpening { get; set; }
        public long IdRoulette { get; set; }
        public DateTime? DateStartOpening { get; set; }
        public DateTime? DateFinishOpening { get; set; }
        public int? NumberOpening { get; set; }
        public string ColorOpening { get; set; }
        public bool EnableOpening { get; set; }
        #endregion Properties
    }
}
