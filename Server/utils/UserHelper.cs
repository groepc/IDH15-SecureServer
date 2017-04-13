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

				try
				{
					MySqlCommand cmd = new MySqlCommand("SELECT id, username, password, passwordsalt, userrole FROM users WHERE username = ?username", con);
					cmd.Parameters.Add(new MySqlParameter("?username", username));

					MySqlDataReader reader = cmd.ExecuteReader();
					if (reader.HasRows == true)
					{
						reader.Read();
						user = new User();
						PopulateUser(reader, user);
					}

				}
				catch (MySqlException)
				{
					throw;
				}

				con.Close();
			}
			return user;
		}

		// Reads the data into an existing user object.
		private void PopulateUser(MySqlDataReader reader, User user)
		{
			//user.Id = int.Parse(reader.GetString("id"));
			user.Name = (string)reader.GetString("username");
			user.Passwordhash = (string)reader.GetString("password");
			user.Passwordsalt = (string)reader.GetString("passwordsalt");
			user.Role = (string)reader.GetString("userrole");
		}
	}
}
