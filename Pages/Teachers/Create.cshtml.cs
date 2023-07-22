using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace universityManagementSystem.Pages.Teachers
{
    public class CreateModel : PageModel
    {

        public Teacher newTeacher = new Teacher();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }


        public void OnPost()
        {
            // Gets form info from page into object
            newTeacher.firstName = Request.Form["firstname"];
            newTeacher.lastName = Request.Form["lastname"];
            newTeacher.department = Request.Form["department"];

            if (newTeacher.firstName.Length == 0 || newTeacher.lastName.Length == 0 || newTeacher.department.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }


            // Store new teacher into database

            try
            {

                String Query = "INSERT INTO teachers (FirstName, LastName, Department) VALUES " +
                                              "('" + newTeacher.firstName+ "', " + "'" + newTeacher.lastName + "', " + "'" + newTeacher.department+"');";

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
                LogMessage.personType = "Teacher";
                LogMessage.action = "Create";
                LogMessage.date = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "";

                LogMessage.message = "New " + LogMessage.personType.ToLower() + " added with values (" + newTeacher.firstName + ", " + newTeacher.lastName + ", " + newTeacher.department + ")";

                String newQuery = "INSERT INTO logs (Person, Action, Date, Message) VALUES " +
                    "('" + LogMessage.personType + "', '" + LogMessage.action + "', '" + LogMessage.date + "', '" + LogMessage.message + "');";

                MySqlCommand cmd = new MySqlCommand(newQuery, conn);

                MySqlDataReader Reader1;

                conn.Open();

                Reader1 = cmd.ExecuteReader();
                conn.Close();








            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Successfully added new teacher";

            newTeacher.firstName="";
            newTeacher.lastName="";
            newTeacher.department="";
        }
    }
}
