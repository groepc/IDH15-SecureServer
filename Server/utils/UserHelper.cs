using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Server.Entities
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
			using (MySqlConnection con = new MySqlConnection(constr))
			{
				con.Open();
				using (MySqlTransaction conTrans = con.BeginTransaction())
				{
					try
					{
						using (MySqlCommand cmd = new MySqlCommand("SELECT id, username, password, passwordsalt, userrole FROM users WHERE username = @Username", con))
						{
							cmd.Transaction = conTrans;
							cmd.Parameters.Add("username", MySqlDbType.VarChar).Value = username;
							cmd.ExecuteNonQuery();
						}
						conTrans.Commit();
					}
					catch (MySqlException)
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
			user.Id = (int)reader["id"];
			user.Name = (string)reader["username"];
			user.Passwordhash = (string)reader["password"];
			user.Passwordsalt = (string)reader["passwordsalt"];
			user.Role = (string)reader["userole"];
		}
	}
}
