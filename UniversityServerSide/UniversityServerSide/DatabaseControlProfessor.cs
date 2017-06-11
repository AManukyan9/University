using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UniversityServerSide
{
    class DatabaseControlProfessor : DatabaseControl
    {
        internal static string AddProfessor(string[] text)  //connects to the 'professorndb' table in 'university' database and adds
        {
            try
            {
                connection.Open();
                MySqlCommand addProfessor = new MySqlCommand
                    (string.Format("INSERT INTO `professordb`(`Name`, `Surname`, `Description`, `Monday`, `Tuesday`, `Wednesday`, `Thursday`, `Friday`, `Saturday`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    text[3], text[4], text[5], text[6], text[7], text[8], text[9], text[10], text[11]), connection);
                MySqlCommand addProfessorUser = new MySqlCommand
                    (string.Format("INSERT INTO `usersdb`(`Username`, `Password`, `Permission`) VALUES ('{0}','{1}','{2}')",
                    text[3] + "." + text[4], Program.RandomStringGenerator(), 19), connection);
                addProfessor.ExecuteNonQuery();
                addProfessorUser.ExecuteNonQuery();
                return "Professor added successfully";

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
        internal static string GradeStudent(string[] text)
        {
            int subjectID = 0;
            int groupID = 0;
            try
            {
                connection.Open();
                MySqlCommand getSubjectID = new MySqlCommand(string.Format("SELECT `ID` FROM `subjectdb` WHERE `Name` = '{0}'", text[5]), connection);
                MySqlCommand getGroupID = new MySqlCommand(string.Format("SELECT `ID` FROM `groupdb` WHERE `Name` = '{0}'", text[3]), connection);
                groupID = Convert.ToInt32(getGroupID.ExecuteScalar());
                Console.WriteLine(groupID);
                subjectID = Convert.ToInt32(getSubjectID.ExecuteScalar());
                MySqlCommand gradeStudent = new MySqlCommand(string.Format("UPDATE `{0} {1}` SET `{2}_{3}`={4} WHERE `student` = {5}", groupID, text[4], subjectID, text[6], text[7], text[8]), connection);
                gradeStudent.ExecuteNonQuery();
                return "Student graded successfully!";
            }
            catch(Exception e)
            {
                return e.Message;
            }
            finally
            {
                connection.Close();
            }
        }
        internal static string GetProfessor(string[] text)  //connects to the 'professorndb' table in 'university' database and adds
        {
            try
            {
                connection.Open();
                MySqlCommand addProfessorUser = new MySqlCommand (string.Format("SELECT `Description` FROM `professordb` WHERE `Name` = '{0}' AND `Surname` = '{1}'",text[3],text[4]), connection);
                return Convert.ToString(addProfessorUser.ExecuteScalar());
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
