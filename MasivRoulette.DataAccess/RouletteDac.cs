using MasivRoulette.DataAccess.ConnectionStrings.Interfaces;
using MasivRoulette.DataAccess.Interfaces;
using MasivRoulette.Entities.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MasivRoulette.DataAccess
{
    public class RouletteDac : IRouletteDac
    {
        #region Constructor - Interfaces
        public IConnectionDac ConnectionDac { get; set; }
        public RouletteDac(IConnectionDac ConnectionDac)
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
        public bool CreateRoulette(Roulette roulette)
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
                        cmd.CommandText = "Roulette_i";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdRoulette", SqlDbType.BigInt);
                        param.Direction = ParameterDirection.Output;

                        param = cmd.Parameters.Add("@RegisterRoulette", SqlDbType.DateTime);
                        param.Direction = ParameterDirection.Output;

                        param = cmd.Parameters.Add("@TitleRoulette", SqlDbType.VarChar, 100);
                        param.Value = roulette.TitleRoulette;
                        #endregion

                        cmd.ExecuteNonQuery();
                        cnn.Close();

                        roulette.IdRoulette = Convert.ToInt64(cmd.Parameters["@IdRoulette"].Value);
                        roulette.RegisterRoulette = Convert.ToDateTime(cmd.Parameters["@RegisterRoulette"].Value);
                        roulette.StateRoulette = true;
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

        public Roulette GetRoulette(long IdRoulette)
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
                        cmd.CommandText = "Roulette_g";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdRoulette", SqlDbType.BigInt);
                        param.Value = IdRoulette;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                return new Roulette(ds.Tables[0].Rows[0]);
                            return new Roulette();
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

        public Roulette ModifyRoulette(Roulette roulette)
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
                        cmd.CommandText = "Roulette_u";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdRoulette", SqlDbType.BigInt);
                        param.Value = roulette.IdRoulette;

                        param = cmd.Parameters.Add("@TitleRoulette", SqlDbType.VarChar, 100);
                        param.Value = roulette.TitleRoulette;

                        param = cmd.Parameters.Add("@StateRoulette", SqlDbType.Bit);
                        param.Value = roulette.StateRoulette;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                return new Roulette(ds.Tables[0].Rows[0]);
                            return new Roulette();
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

        public DataSet GetAllRoulettes(out int countRoulettes)
        {
            countRoulettes = 0;
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
                        cmd.CommandText = "Roulettes_g_All";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@RowsCount", SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            countRoulettes = Convert.ToInt32(cmd.Parameters["@RowsCount"].Value);
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
