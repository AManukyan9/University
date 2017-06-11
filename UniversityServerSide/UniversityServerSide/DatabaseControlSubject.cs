using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UniversityServerSide
{
    class DatabaseControlSubject : DatabaseControl
    {
        internal static string AddSubject(string[] text)    //connects to the 'subjectdb' table in 'university' database and adds
        {
            try
            {
                connection.Open();
                MySqlCommand addSubject = new MySqlCommand
                    (string.Format("INSERT INTO `subjectdb`(`ID`, `Name`) VALUES ('{0}')",
                    text[3]), connection);
                addSubject.ExecuteNonQuery();
                return "Subject added successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
