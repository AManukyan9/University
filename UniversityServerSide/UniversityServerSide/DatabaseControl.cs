using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace UniversityServerSide
{
    class DatabaseControl
    {
        private static string conString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;  //retrieves connection string from App.config
        protected static MySqlConnection connection = new MySqlConnection(conString);   //database connection init.
        protected static MySqlConnection connectionSide = new MySqlConnection(conString);   //database connection init.
    }
}
