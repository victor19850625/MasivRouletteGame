using MasivRoulette.DataAccess.ConnectionStrings.Interfaces;
using MasivRoulette.DataAccess.Interfaces;
using MasivRoulette.Entities.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MasivRoulette.DataAccess
{
    public class BetDac : IBetDac
    {
        #region Constructor - Interfaces
        public IConnectionDac ConnectionDac { get; set; }
        public BetDac(IConnectionDac ConnectionDac)
        {
            this.ConnectionDac = ConnectionDac;
        }
        #endregion Constructor - Interfaces

        #region Properties
        public string Message_error { get; set; }
        private void cnn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            string error_message = e.Message;
            if (e.Errors != null)
            {
                if (e.Errors.Count > 0)
                {
                    error_message += string.Join("|", e.Errors);
                }
            }
            this.Message_error = error_message;
        }
        #endregion  Properties

        #region Methods
        public bool CreateBet(Bet bet)
        {
            using (SqlConnection cnn = new(this.ConnectionDac.GetConnection(true)))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cnn.InfoMessage += cnn_InfoMessage;
                        cnn.FireInfoMessageEventOnUserErrors = true;
                        cnn.Open();
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Bet_i";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdBet", SqlDbType.BigInt);
                        param.Direction = ParameterDirection.Output;

                        param = cmd.Parameters.Add("@RegisterBet", SqlDbType.DateTime);
                        param.Direction = ParameterDirection.Output;

                        param = cmd.Parameters.Add("@IdOpening", SqlDbType.BigInt);
                        param.Value = bet.IdOpening;

                        param = cmd.Parameters.Add("@IdUser", SqlDbType.BigInt);
                        param.Value = bet.IdUser;

                        param = cmd.Parameters.Add("@NumberBet", SqlDbType.SmallInt);
                        param.Value = bet.NumberBet;

                        param = cmd.Parameters.Add("@ColorBet", SqlDbType.VarChar, 1);
                        param.Value = bet.ColorBet;

                        param = cmd.Parameters.Add("@ValueBet", SqlDbType.Decimal);
                        param.Value = bet.ValueBet;
                        #endregion

                        cmd.ExecuteNonQuery();
                        cnn.Close();

                        bet.IdBet = Convert.ToInt64(cmd.Parameters["@IdBet"].Value);
                        bet.RegisterBet = Convert.ToDateTime(cmd.Parameters["@RegisterBet"].Value);


                        if (this.Message_error != null)
                            throw new Exception(this.Message_error);
                    }
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
        }

        public Bet GetBet(long idBet)
        {
            using (SqlConnection cnn = new(this.ConnectionDac.GetConnection(true)))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cnn.InfoMessage += cnn_InfoMessage;
                        cnn.FireInfoMessageEventOnUserErrors = true;
                        cnn.Open();
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Bet_g";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdBet", SqlDbType.BigInt);
                        param.Value = idBet;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                return new Bet(ds.Tables[0].Rows[0]);
                            return new Bet();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
        }

        public DataSet GetBetsIdOpening(long idOpening)
        {
            using (SqlConnection cnn = new(this.ConnectionDac.GetConnection(true)))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cnn.InfoMessage += cnn_InfoMessage;
                        cnn.FireInfoMessageEventOnUserErrors = true;
                        cnn.Open();
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "BetsIdOpening_g";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdOpening", SqlDbType.BigInt);
                        param.Value = idOpening;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            return ds;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
        }

        public DataSet GetWinnersBet(Bet bet)
        {
            using (SqlConnection cnn = new(this.ConnectionDac.GetConnection(true)))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cnn.InfoMessage += cnn_InfoMessage;
                        cnn.FireInfoMessageEventOnUserErrors = true;
                        cnn.Open();
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "BetsWinners_g";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdOpening", SqlDbType.BigInt);
                        param.Value = bet.IdOpening;

                        param = cmd.Parameters.Add("@NumberBet", SqlDbType.SmallInt);
                        param.Value = bet.IdOpening;

                        param = cmd.Parameters.Add("@ColorBet", SqlDbType.VarChar, 1);
                        param.Value = bet.IdOpening;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            return ds;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
        }
        #endregion  Methods
    }
}
