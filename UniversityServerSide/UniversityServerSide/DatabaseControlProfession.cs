using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UniversityServerSide
{
    class DatabaseControlProfession : DatabaseControl
    {
        internal static string AddProfession(string[] text) //connects to the 'professiondb' table in 'university' database and adds
        {
            try
            {
                connection.Open();
                MySqlCommand checkName = new MySqlCommand(string.Format("SELECT COUNT(*) FROM `professiondb` WHERE `Name` = '{0}'", text[3]), connection);
                int professionCount = Convert.ToInt32(checkName.ExecuteScalar());
                if (professionCount > 0)
                {
                    return "Profession already exists";
                }
                MySqlCommand addProfession = new MySqlCommand
                    (string.Format("INSERT INTO `professiondb`(`Name`, `Prof. Subj. 1.1`, `Non-Prof. Subj. 1.1`, `Prof. Subj. 1.2`, `Non-Prof. Subj. 1.2`, `Prof. Subj. 2.1`, `Non-Prof. Subj. 2.1`, `Prof. Subj. 2.2`, `Non-Prof. Subj. 2.2`, `Prof. Subj. 3.1`, `Non-Prof. Subj. 3.1`, `Prof. Subj. 3.2`, `Non-Prof. Subj. 3.2`, `Prof. Subj. 4.1`, `Non-Prof. Subj. 4.1`, `Prof. Subj. 4.2`, `Non-Prof. Subj. 4.2`, `Description`, `Faculty`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                    text[3], text[4], text[5], text[6], text[7], text[8], text[9], text[10], text[11], text[12], text[13], text[14], text[15], text[16], text[17], text[18], text[19], text[20], text[21]), connection);
                addProfession.ExecuteNonQuery();
                MySqlCommand getID = new MySqlCommand(string.Format("SELECT `ID` FROM `professiondb` WHERE `Name` = '{0}'", text[3]), connection);
                int id = Convert.ToInt32(getID.ExecuteScalar());
                MySqlCommand updateFaculty = new MySqlCommand(string.Format("UPDATE `facultydb` SET `Professions`= CONCAT(`Professions`, ',' , '{0}' ) WHERE `ID`= {1}", id, text[21]), connection);
                updateFaculty.ExecuteNonQuery();
                return "Profession added successfully";
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
        internal static string SelectProfession(string[] text)
        {
            try
            {
                connection.Open();
                MySqlCommand selectProfession = new MySqlCommand(
                    string.Format("SELECT `Name`, `Prof. Subj. 1.1`, `Non-Prof. Subj. 1.1`, `Prof. Subj. 1.2`, `Non-Prof. Subj. 1.2`, `Prof. Subj. 2.1`, `Non-Prof. Subj. 2.1`, `Prof. Subj. 2.2`, `Non-Prof. Subj. 2.2`, `Prof. Subj. 3.1`, `Non-Prof. Subj. 3.1`, `Prof. Subj. 3.2`, `Non-Prof. Subj. 3.2`, `Prof. Subj. 4.1`, `Non-Prof. Subj. 4.1`, `Prof. Subj. 4.2`, `Non-Prof. Subj. 4.2`, `Description`, `Groups`, `Faculty` FROM `professiondb` WHERE `Name` = '{0}'", text[3]), connection);
                MySqlCommand getFaculty;
                string profession = "";
                using (MySqlDataReader rd = selectProfession.ExecuteReader())
                {
                    rd.Read();
                    profession = rd["Name"].ToString() 
                        + ";" + rd["Prof. Subj. 1.1"].ToString() + ";" + rd["Non-Prof. Subj. 1.1"].ToString() + ";" + rd["Prof. Subj. 1.2"].ToString() + ";" + rd["Non-Prof. Subj. 1.2"].ToString()
                        + ";" + rd["Prof. Subj. 2.1"].ToString() + ";" + rd["Non-Prof. Subj. 2.1"].ToString() + ";" + rd["Prof. Subj. 2.2"].ToString() + ";" + rd["Non-Prof. Subj. 2.2"].ToString()
                        + ";" + rd["Prof. Subj. 3.1"].ToString() + ";" + rd["Non-Prof. Subj. 3.1"].ToString() + ";" + rd["Prof. Subj. 3.2"].ToString() + ";" + rd["Non-Prof. Subj. 3.2"].ToString()
                        + ";" + rd["Prof. Subj. 4.1"].ToString() + ";" + rd["Non-Prof. Subj. 4.1"].ToString() + ";" + rd["Prof. Subj. 4.2"].ToString() + ";" + rd["Non-Prof. Subj. 4.2"].ToString()
                        + ";" + rd["Description"].ToString() + ";";
                    getFaculty = new MySqlCommand(string.Format("SELECT `Name` FROM `facultydb` WHERE `ID` = {0}", rd["Faculty"].ToString()), connection);
                }
                profession += Convert.ToString(getFaculty.ExecuteScalar());
                Console.WriteLine(profession);
                return profession;
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
