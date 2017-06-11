using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UniversityServerSide
{
    class DatabaseControlGroup : DatabaseControl
    {
        internal static string AddGroup(string[] text)  //connects to the 'groupdb' table in 'university' database and adds
        {
            try
            {
                connection.Open();
                MySqlCommand getFaculty = new MySqlCommand(string.Format("SELECT `Faculty` FROM `professiondb` WHERE `ID` = {0}", text[10]), connection);
                int faculty = Convert.ToInt32(getFaculty.ExecuteScalar());
                MySqlCommand addGroup = new MySqlCommand
                    (string.Format("INSERT INTO `groupdb`(`Name`, `Monday`, `Thuesday`, `Wednesday`, `Thursday`, `Friday`, `Saturday`, `Profession`, `Faculty`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8})",
                    text[3], text[4], text[5], text[6], text[7], text[8], text[9], text[10], faculty), connection);
                MySqlCommand updateFaculty = new MySqlCommand(string.Format("UPDATE `professiondb` SET `Groups` = CONCAT(`Groups`, ',' , '{0}') WHERE `Name` = '{1}'", text[3], text[10]), connection);
                addGroup.ExecuteNonQuery();
                updateFaculty.ExecuteNonQuery();
                return "Group added successfully";
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

        internal static string CreateGradeBook(string[] text)  //connects to the 'groupdb' table in 'university' database and adds
        {
            try
            {
                connection.Open();
                string table = string.Format("CREATE TABLE `{0} {1}` (student TEXT", text[3], text[4]);
                MySqlCommand getProfession = new MySqlCommand(string.Format("SELECT `Profession` FROM `groupdb` WHERE ID = {0}", text[3]), connection);
                int profession = Convert.ToInt32(getProfession.ExecuteScalar());
                MySqlCommand getProf = new MySqlCommand(string.Format("SELECT `Prof. Subj. {0}` FROM `professiondb` WHERE `ID` = {1}", text[4], profession), connection);
                MySqlCommand getNonProf = new MySqlCommand(string.Format("SELECT `Non-Prof. Subj. {0}` FROM `professiondb` WHERE `ID` = {1}", text[4], profession), connection);
                string[] prof = Convert.ToString(getProf.ExecuteScalar()).Split(',');
                string[] nonProf = Convert.ToString(getNonProf.ExecuteScalar()).Split(',');
                Console.WriteLine(getNonProf.ExecuteScalar());
                for (int i = 1; i < prof.Length; i++)
                {
                    table += string.Format(", `{0}_midterm1` INT, `{0}_midterm2` INT, `{0}_exam` INT", prof[i]);
                }
                Console.WriteLine(nonProf.Length);
                for (int i = 1; i < nonProf.Length; i++)
                {
                    table += string.Format(", `{0}_pass` INT", nonProf[i]);
                }
                table += ");";
                MySqlCommand createGradeBook = new MySqlCommand(table, connection);
                createGradeBook.ExecuteNonQuery();
                Console.WriteLine(table);
                MySqlCommand getStudents = new MySqlCommand(string.Format("SELECT `Students` FROM `groupdb` WHERE `ID` = {0}", text[3]), connection);
                string[] students = Convert.ToString(getStudents.ExecuteScalar()).Split(',');
                MySqlCommand insertStudent;
                for (int i = 1; i < students.Length; i++)
                {
                    insertStudent = new MySqlCommand(string.Format("INSERT INTO `{0} {1}`(`student`) VALUES ('{2}')", text[3], text[4], students[i]), connection);
                    insertStudent.ExecuteNonQuery();
                }
                return "Gradebook created successfully";
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
        internal static string ShowGradeBook(string[] text)  //connects to the 'groupdb' table in 'university' database and adds
        {
            try
            {
                connection.Open();
                MySqlCommand getGroup = new MySqlCommand(string.Format("SELECT `Group` FROM `studentdb` WHERE `ID` = {0}", text[3]), connection);
                string group = Convert.ToString(getGroup.ExecuteScalar());
                MySqlCommand getProfession = new MySqlCommand(string.Format("SELECT `Profession` FROM `groupdb` WHERE `ID` = {0}", group), connection);
                string profession = Convert.ToString(getProfession.ExecuteScalar());
                MySqlCommand getSubjectProf = new MySqlCommand(string.Format("SELECT `Prof. Subj. {0}` FROM `professiondb` WHERE `ID` = {1}", text[4], group), connection);
                MySqlCommand getSubjectNonProf = new MySqlCommand(string.Format("SELECT `Non-Prof. Subj. {0}` FROM `professiondb` WHERE `ID` = {1}", text[4], group), connection);
                MySqlCommand grade;

                MySqlCommand getSubject;
                string[] ProfSubject = Convert.ToString(getSubjectProf.ExecuteScalar()).Split(',');
                string[] NonProfSubject = Convert.ToString(getSubjectNonProf.ExecuteScalar()).Split(',');
                string subject;
                string result = "";
                for (int i = 1; i < ProfSubject.Length; i++)
                {
                    getSubject = new MySqlCommand(string.Format("SELECT `Name` FROM `subjectdb` WHERE `ID` = {0}", ProfSubject[i]), connection);
                    subject = Convert.ToString(getSubject.ExecuteScalar());
                    grade = new MySqlCommand(string.Format("SELECT `{0}_midterm1` FROM `{1} {2}` WHERE `student` = {3}", ProfSubject[i], group, text[4], text[3]), connection);
                    result += subject + " midterm 1" + " - " + Convert.ToString(grade.ExecuteScalar()) + ";";
                    grade = new MySqlCommand(string.Format("SELECT `{0}_midterm2` FROM `{1} {2}` WHERE `student` = {3}", ProfSubject[i], group, text[4], text[3]), connection);
                    result += subject + " midterm 2" + " - " + Convert.ToString(grade.ExecuteScalar()) + ";";
                    grade = new MySqlCommand(string.Format("SELECT `{0}_exam` FROM `{1} {2}` WHERE `student` = {3}", ProfSubject[i], group, text[4], text[3]), connection);
                    result += subject + " exam" + " - " + Convert.ToString(grade.ExecuteScalar()) + ";";
                }
                for (int i = 1; i < NonProfSubject.Length; i++)
                {
                    getSubject = new MySqlCommand(string.Format("SELECT `Name` FROM `subjectdb` WHERE `ID` = {0}", NonProfSubject[i]), connection);
                    subject = Convert.ToString(getSubject.ExecuteScalar());
                    grade = new MySqlCommand(string.Format("SELECT `{0}_pass` FROM `{1} {2}` WHERE `student` = {3}", NonProfSubject[i], group, text[4], text[3]), connection);
                    result += subject + " exam" + " - " + Convert.ToString(grade.ExecuteScalar()) + ";";
                }
                return result;
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
