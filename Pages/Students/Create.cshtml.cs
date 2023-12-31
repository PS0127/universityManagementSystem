using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;
using System.Net.Security;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace universityManagementSystem.Pages.Students
{
    public class CreateStudentModel : PageModel
    {
        public Student newStudent = new Student();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }


        public void OnPost()
        {
            // Gets form info from page into object
            newStudent.firstName = Request.Form["firstname"];
            newStudent.lastName = Request.Form["lastname"];
            newStudent.course = Request.Form["course"];

            if (newStudent.firstName.Length == 0 || newStudent.lastName.Length == 0 || newStudent.course.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }


            // Store new student into database

            try
            {

                String Query = "INSERT INTO students (FirstName, LastName, Course) VALUES " +
                                              "('" + newStudent.firstName+ "', " + "'" + newStudent.lastName + "', " + "'" + newStudent.course+"');";

                MySqlConnection conn = new MySqlConnection(Connection.conn);

                MySqlCommand command = new MySqlCommand(Query, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command.ExecuteReader();

                
                while (Reader.Read())
                {

                }

                conn.Close();

                // make new log message object and set data
                LogMessage LogMessage = new LogMessage();
                LogMessage.personType = "Student";
                LogMessage.action = "Create";
                LogMessage.date = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "";
                
                LogMessage.message = "New " + LogMessage.personType.ToLower() + " added with values (" + newStudent.firstName + ", " + newStudent.lastName + ", " + newStudent.course + ")";

                String newQuery = "INSERT INTO logs (Person, Action, Date, Message) VALUES " +
                    "('" + LogMessage.personType + "', '" + LogMessage.action + "', '" + LogMessage.date + "', '" + LogMessage.message + "');";

                MySqlCommand cmd = new MySqlCommand(newQuery, conn);

                MySqlDataReader Reader1;

                conn.Open();

                Reader1 = cmd.ExecuteReader();
                conn.Close();

                






            }

            catch ( Exception ex )
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Successfully added new student";

            newStudent.firstName="";
            newStudent.lastName="";
            newStudent.course="";
        }
    }
}
