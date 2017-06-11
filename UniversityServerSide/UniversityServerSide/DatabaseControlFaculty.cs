using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UniversityServerSide
{
    class DatabaseControlFaculty : DatabaseControl
    {
        internal static string AddFaculty(string[] text)    //connects to the 'facultydb' table in 'university' database and adds
        {
            try
            {
                connection.Open();
                MySqlCommand checkName = new MySqlCommand(string.Format("SELECT COUNT(*) FROM `facultydb` WHERE `Name` = '{0}'", text[3]), connection);
                int facultyCount = Convert.ToInt32(checkName.ExecuteScalar());
                if (facultyCount > 0)
                {
                    return "Faculty already exists";
                }
                MySqlCommand addFaculty = new MySqlCommand
                    (string.Format("INSERT INTO `facultydb`(`Name`, `Payment`, `Description`) VALUES ('{0}',{1},'{2}')",
                    text[3], Convert.ToDecimal(text[4]), text[5]), connection);
                addFaculty.ExecuteNonQuery();
                return "Faculty added successfully";
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
        internal static string SelectFaculty(string[] text)
        {
            try
            {
                connection.Open();
                MySqlCommand selectFaculty = new MySqlCommand(string.Format("SELECT `Name`, `Payment`, `Professions`, `Description` FROM `facultydb` WHERE `ID` =  {0}", text[3]), connection);
                MySqlCommand getProfession;
                string faculty = "";
                string professionsstr = "";
                using (MySqlDataReader rd = selectFaculty.ExecuteReader())
                {
                    rd.Read();
                    faculty = rd["Name"].ToString() + ";" + rd["Payment"].ToString() + ";" + rd["Description"].ToString() + ";";
                    professionsstr = rd["Professions"].ToString();
                }
                string[] professions = professionsstr.Split(',');
                string professionName = "";
                for(int i = 1; i < professions.Length; i++)
                {
                    getProfession = new MySqlCommand(string.Format("SELECT `Name` FROM `facultydb` WHERE `ID` = {0}", professions[i]), connection);
                    professionName += "," + Convert.ToString(getProfession.ExecuteScalar());
                }
                faculty += professionName;
                return faculty;
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
