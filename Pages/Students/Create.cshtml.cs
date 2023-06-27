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
                String connectionString = "Server=managementsystem.cac8ficgdlsa.us-east-1.rds.amazonaws.com,3306; Database=sys; User Id = admin; Password=vPvdHKV4Ac8zC2uP";

                String Query = "INSERT INTO students (FirstName, LastName, Course) VALUES " +
                                              "('" + newStudent.firstName+ "', " + "'" + newStudent.lastName + "', " + "'" + newStudent.course+"');";

                MySqlConnection conn = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand(Query, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command.ExecuteReader();

                
                while (Reader.Read())
                {

                }
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
