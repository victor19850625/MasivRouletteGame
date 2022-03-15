using System;
using System.Data;
using System.Data.SqlClient;
using MasivRoulette.Entities.Models;
using MasivRoulette.DataAccess.Interfaces;

using MasivRoulette.DataAccess.ConnectionStrings.Interfaces;
namespace MasivRoulette.DataAccess
{
    public class OpeningDac : IOpeningDac
    {
        #region Constructor - Interfaces
        public IConnectionDac ConnectionDac { get; set; }
        public OpeningDac(IConnectionDac ConnectionDac)
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
        public bool OpenOpening(Opening opening)
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
                        cmd.CommandText = "OpeningOpen";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdOpening", SqlDbType.BigInt);
                        param.Direction = ParameterDirection.Output;

                        param = cmd.Parameters.Add("@DateStartOpening", SqlDbType.DateTime);
                        param.Direction = ParameterDirection.Output;

                        param = cmd.Parameters.Add("@IdRoulette", SqlDbType.BigInt);
                        param.Value = opening.IdRoulette;
                        #endregion

                        cmd.ExecuteNonQuery();
                        cnn.Close();

                        opening.IdOpening = Convert.ToInt64(cmd.Parameters["@IdOpening"].Value);
                        opening.DateStartOpening = Convert.ToDateTime(cmd.Parameters["@DateStartOpening"].Value);
                        opening.EnableOpening = true;
                        opening.NumberOpening = null;
                        opening.ColorOpening = null;

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

        public Opening GetOpening(long idOpening)
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
                        cmd.CommandText = "Opening_g";

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
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                return new Opening(ds.Tables[0].Rows[0]);
                            return new Opening();
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

        public Opening CloseOpening(Opening opening)
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
                        cmd.CommandText = "OpeningClose";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdOpening", SqlDbType.BigInt);
                        param.Value = opening.IdOpening;

                        param = cmd.Parameters.Add("@NumberOpening", SqlDbType.SmallInt);
                        param.Value = opening.NumberOpening;

                        param = cmd.Parameters.Add("@ColorOpening", SqlDbType.VarChar, 1);
                        param.Value = opening.ColorOpening;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                return new Opening(ds.Tables[0].Rows[0]);
                            return new Opening();
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
