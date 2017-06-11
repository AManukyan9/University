using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace UniversityServerSide
{
    class UserControl
    {
        private static string session = "";
        private static string conString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;  //retrieves connection string from App.config
        protected static MySqlConnection connection = new MySqlConnection(conString);   //database connection init.
        internal static Dictionary<string, string> users = new Dictionary<string, string>();
        private static string GenerateSession()
        {
            string session = Program.RandomStringGenerator();
            return session;
        }

        internal static string Login(string[] text)
        {
            
            try
            {
                connection.Open();
                MySqlCommand selectLogin = new MySqlCommand(string.Format("SELECT COUNT(*) FROM `usersdb` WHERE `Username` = '{0}' AND `Password` = '{1}'", text[1], text[2]), connection);
                if (Convert.ToInt32(selectLogin.ExecuteScalar()) != 0)
                    session = GenerateSession();
                else
                    return "Login Failed";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                connection.Close();
            }
            if (users.ContainsKey(text[1]))
            {
                users[text[1]] = session;
            }
            else
            {
                users.Add(text[1], session);
            }
            return session;
        }

        internal static string Logout(string[] text)
        {
            try
            {
                users.Remove(text[1]);
                session = "";
                return "logged out";
            }
            catch
            {
                return "logout failed";
            }
        }

        internal static short checkSession(string[] text)
        {
            if (!users.ContainsKey(text[1]))
                return 0;
            if ((users[text[1]] == text[2]) && (!text[2].Equals("")))
            {
                try
                {
                    connection.Open();
                    MySqlCommand getPermission = new MySqlCommand(string.Format("SELECT `Permission` FROM `usersdb` WHERE `Username` = '{0}'", text[1]), connection);
                    return Convert.ToInt16(getPermission.ExecuteScalar());
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }
            else
                return 0;
        }
    }
}
