using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UniversityServerSide
{
    class DatabaseControlStudent : DatabaseControl
    {
        internal static string AddStudent(string[] text)    //connects to the 'studentdb' table in 'university' database and adds
        {
            try
            {
                connection.Open();

                MySqlCommand addStudent = new MySqlCommand
                    (string.Format("INSERT INTO `studentdb`(`Name`, `Surname`, `Description`, `Group`) VALUES ('{0}','{1}','{2}','{3}')",
                    text[3], text[4], text[5], text[6]), connection);
                MySqlCommand getID = new MySqlCommand("SELECT LAST_INSERT_ID();", connection);
                addStudent.ExecuteNonQuery();
                string id = Convert.ToString(getID.ExecuteScalar());
                MySqlCommand addStudentUser = new MySqlCommand
                (string.Format("INSERT INTO `usersdb`(`Username`, `Password`, `Permission`, `studentID`) VALUES ('{0}','{1}','{2}','{3}')",
                text[3] + "." + text[4], Program.RandomStringGenerator(), 0, id), connection);
                addStudentUser.ExecuteNonQuery();
                return id;
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
        internal static string AddStudentToGroup(string[] text)
        {
            try
            {
                connection.Open();
                MySqlCommand getProfession = new MySqlCommand(string.Format("SELECT `Profession` FROM `groupdb` WHERE ID = {0}", text[4]), connection);
                int profession = Convert.ToInt32(getProfession.ExecuteScalar());
                MySqlCommand getFaculty = new MySqlCommand(string.Format("SELECT `Faculty` FROM `groupdb` WHERE ID = {0}", text[4]), connection);
                int faculty = Convert.ToInt32(getFaculty.ExecuteScalar());
                MySqlCommand updateStudent = new MySqlCommand(string.Format("UPDATE `studentdb` SET `Group` = '{0}', `Profession`= '{1}',`Faculty`= '{2}' WHERE `ID` = {3}", text[4], profession, faculty, text[3]), connection);
                updateStudent.ExecuteNonQuery();
                MySqlCommand updateGroup = new MySqlCommand(string.Format("UPDATE `groupdb` SET `Students`= CONCAT(`Students`, ',', '{0}') WHERE `ID` = {1}", text[3], text[4]), connection);
                updateGroup.ExecuteNonQuery();
                return "Student added to group succesfully";
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

        internal static List<string> RetrieveStudents(string[] text)
        {
            string retrieved = "";
            List<string> result = new List<string>();
            try
            {
                connection.Open();
                connectionSide.Open();
                MySqlCommand cmd = new MySqlCommand(string.Format("SELECT * FROM `studentdb` WHERE `Name`= '{0}' AND `Surname` = '{1}'", text[3], text[4]), connection);
                MySqlCommand getGroup;
                MySqlCommand getProfession;
                MySqlCommand getFaculty;
                string groupID;
                string professionID;
                string facultyID;
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        retrieved += rd["Name"].ToString() + "," + rd["Surname"].ToString() + "," + rd["ID"].ToString() + "," + rd["Description"].ToString() + ",";
                        groupID = rd["Group"].ToString();
                        professionID = rd["Profession"].ToString();
                        facultyID = rd["Faculty"].ToString();
                        getGroup = new MySqlCommand(string.Format("SELECT `Name` FROM `groupdb` WHERE `ID` = {0}",groupID), connectionSide);
                        getProfession = new MySqlCommand(string.Format("SELECT `Name` FROM `professiondb` WHERE `ID` = {0}",professionID), connectionSide);
                        getFaculty = new MySqlCommand(string.Format("SELECT `Name` FROM `facultydb` WHERE `ID` = {0}",facultyID), connectionSide);
                        retrieved += Convert.ToString(getFaculty.ExecuteScalar()) + "," + Convert.ToString(getProfession.ExecuteScalar()) + "," + Convert.ToString(getGroup.ExecuteScalar()) ;
                        result.Add(retrieved);
                        retrieved = "";
                    }
                }
                return result;
            }
           
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                connectionSide.Close();
            }
        }
        internal static string getStudentByUser(string[] text)
        {
            try
            {
                connection.Open();
                MySqlCommand getStudent = new MySqlCommand(string.Format("SELECT  `studentID` FROM `usersdb` WHERE `Username` = '{0}'", text[1]), connection);
                return Convert.ToString(getStudent.ExecuteScalar());
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
