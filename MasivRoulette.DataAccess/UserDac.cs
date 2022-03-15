using MasivRoulette.DataAccess.ConnectionStrings.Interfaces;
using MasivRoulette.DataAccess.Interfaces;
using MasivRoulette.Entities.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MasivRoulette.DataAccess
{
    public class UserDac : IUserDac
    {
        #region Constructor - Interfaces
        public IConnectionDac ConnectionDac { get; set; }
        public UserDac(IConnectionDac ConnectionDac)
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
        #endregion Properties

        #region Methods
        public bool CreateUser(User user)
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
                        cmd.CommandText = "User_i";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdUser", SqlDbType.BigInt);
                        param.Direction = ParameterDirection.Output;

                        param = cmd.Parameters.Add("@RegisterUser", SqlDbType.DateTime);
                        param.Direction = ParameterDirection.Output;

                        param = cmd.Parameters.Add("@NameUser", SqlDbType.VarChar, 100);
                        param.Value = user.NameUser;

                        param = cmd.Parameters.Add("@EmailUser", SqlDbType.VarChar, 200);
                        param.Value = user.EmailUser;

                        param = cmd.Parameters.Add("@CreditUser", SqlDbType.Decimal);
                        param.Value = user.CreditUser;
                        #endregion

                        cmd.ExecuteNonQuery();
                        cnn.Close();

                        user.IdUser = Convert.ToInt64(cmd.Parameters["@IdUser"].Value);
                        user.RegisterUser = Convert.ToDateTime(cmd.Parameters["@RegisterUser"].Value);
                        user.StateUser = true;

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

        public User GetUser(long idUser)
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
                        cmd.CommandText = "User_g";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdUser", SqlDbType.BigInt);
                        param.Value = idUser;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                return new User(ds.Tables[0].Rows[0]);
                            return new User();
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

        public User ModifyUser(User user)
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
                        cmd.CommandText = "User_u";

                        #region PARAMETERS
                        SqlParameter param = cmd.Parameters.Add("@IdUser", SqlDbType.BigInt);
                        param.Value = user.IdUser;

                        param = cmd.Parameters.Add("@NameUser", SqlDbType.VarChar, 100);
                        param.Value = user.NameUser;

                        param = cmd.Parameters.Add("@EmailUser", SqlDbType.VarChar, 200);
                        param.Value = user.EmailUser;

                        param = cmd.Parameters.Add("@CreditUser", SqlDbType.Decimal);
                        param.Value = user.CreditUser;

                        param = cmd.Parameters.Add("@StateUser", SqlDbType.Bit);
                        param.Value = user.StateUser;
                        #endregion

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }
                            cnn.Close();
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                return new User(ds.Tables[0].Rows[0]);
                            return new User();
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
        #endregion Methods
    }
}
