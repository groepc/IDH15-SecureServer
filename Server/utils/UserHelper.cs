using Server.entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Server.utils
{
    public class UserHelper
    {
        private readonly string _userHelper;

        public UserHelper()
        {
            _userHelper = ConfigurationManager.ConnectionStrings["logindb"].ConnectionString;
        }

        public User GetByName(string username)
        {
            User user = null;
            string constr = ConfigurationManager.ConnectionStrings["logindb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlTransaction conTrans = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PasswordHash, PasswordSalt, Role FROM [users] WHERE Name = @Username"))
                        {
                            cmd.Transaction = conTrans;
                            cmd.Parameters.Add("username", SqlDbType.VarChar).Value = username;
                            cmd.ExecuteNonQuery();
                        }
                        conTrans.Commit();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                }
                con.Close();
            }
            return user;
        }

        // Reads the data into an existing user object.
        private void PopulateUser(IDataReader reader, User user)
        {
            user.Id = (int)reader["Id"];
            user.Name = (string)reader["Name"];
            user.Passwordhash = (string)reader["Passwordhash"];
            user.Passwordsalt = (string)reader["Passwordsalt"];
            user.Role = (string)reader["Role"];
        }
    }
}
