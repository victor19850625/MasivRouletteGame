using System;
using System.Data;

namespace MasivRoulette.Entities.Models
{
    public class User
    {
        #region Constructor
        public User() 
        { 
        }
        public User(long idUser, decimal creditUser)
        {
            IdUser = idUser;
            CreditUser = creditUser;
            EmailUser = null;
            StateUser = null;
        }
        public User(DataRow user_dr)
        {
            this.IdUser = Convert.ToInt64(user_dr["IdUser"]);
            this.RegisterUser = Convert.ToDateTime(user_dr["RegisterUser"]);
            this.NameUser = Convert.ToString(user_dr["NameUser"]);
            this.EmailUser = Convert.ToString(user_dr["EmailUser"]);
            this.CreditUser = Convert.ToDecimal(user_dr["CreditUser"]);
            this.StateUser = Convert.ToBoolean(user_dr["StateUser"]);
        }
        #endregion Constructor

        #region Properties
        public long IdUser { get; set; }
        public DateTime RegisterUser { get; set; }
        public string NameUser { get; set; }
        public string EmailUser { get; set; }
        public decimal? CreditUser { get; set; }
        public bool? StateUser { get; set; }
        #endregion Properties
    }
}
