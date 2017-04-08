using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Server.utils
{
    class Login
    {
        public void ValidateUser()
        {
            //int userId = 0;
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand("Validate_User"))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@Username", username);
            //        cmd.Parameters.AddWithValue("@Password", password);
            //        cmd.Connection = con;
            //        con.Open();
            //        userId = Convert.ToInt32(cmd.ExecuteScalar());
            //        con.Close();
            //    }
            //    switch (userId)
            //    {
            //        case -1:
            //            FailureText = "Username and/or password is incorrect.";
            //            break;
            //        case -2:
            //            FailureText = "Account has not been activated.";
            //            break;
            //        default:
            //            FormsAuthentication.RedirectFromLoginPage(UserName, RememberMeSet);
            //            break;
            //    }
            }
        }
    }
