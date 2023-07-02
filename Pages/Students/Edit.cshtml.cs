using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace universityManagementSystem.Pages.Students
{
    public class EditModel : PageModel
    {
        public Student editStudent = new Student();

        public string errorMessage = "";
        public string successMessage = "";


        public void OnGet()
        {
            String ID = Request.Query["id"];

            

            try
            {
                String connectionString = "Server=managementsystem.cac8ficgdlsa.us-east-1.rds.amazonaws.com,3306; Database=sys; User Id = admin; Password=vPvdHKV4Ac8zC2uP";

                String Query = "SELECT * FROM students WHERE StudentID = " + ID;

                MySqlConnection conn = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand(Query, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command.ExecuteReader();


                while (Reader.Read())
                { 
                    // Saves database contents into new object for page to show
                    editStudent.firstName = Reader.GetString(1);
                    editStudent.lastName = Reader.GetString(2);
                    editStudent.course = Reader.GetString(3);


                }
                conn.Close();


            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Checks whether student info is null or empty which means no student found
            if(editStudent.firstName == null || editStudent.firstName.Length == 0)
            {
                errorMessage = "No student found";
                return;
            }


        }

        public void OnPost()
        {
            String ID = Request.Query["id"];

            editStudent.firstName = Request.Form["firstname"];
            editStudent.lastName = Request.Form["lastname"];
            editStudent.course = Request.Form["course"];


            try
            {
                String connectionString = "Server=managementsystem.cac8ficgdlsa.us-east-1.rds.amazonaws.com,3306; Database=sys; User Id = admin; Password=vPvdHKV4Ac8zC2uP";


                // get old student data

                String Query2 = "SELECT * FROM students WHERE StudentID = " + ID;

                MySqlConnection conn = new MySqlConnection(connectionString);

                MySqlCommand command1 = new MySqlCommand(Query2, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command1.ExecuteReader();

                Student logStudent = new Student();

                while (Reader.Read())
                {
                    // Saves database contents into new object for page to show
                    logStudent.firstName = Reader.GetString(1);
                    logStudent.lastName = Reader.GetString(2);
                    logStudent.course = Reader.GetString(3);


                }
                conn.Close();



                // Store new student data into database
                Debug.WriteLine("course is "+editStudent.course);
                string Query = "";
                if (editStudent.course != null)
                {
  
                    Query = "UPDATE students SET" +
                        " FirstName = '" + editStudent.firstName + "'," +
                        " LastName = '" + editStudent.lastName + "'," +
                        " Course = '" + editStudent.course+ "'" +
                        " WHERE StudentID = " + ID + ";";

                }
                else 
                {
                    
                    Query = "UPDATE students SET" +
                        " FirstName = '" + editStudent.firstName + "'," +
                        " LastName = '" + editStudent.lastName + "'" +
                        " WHERE StudentID = " + ID + ";";
                }


                Debug.WriteLine(Query);

                MySqlCommand command = new MySqlCommand(Query, conn);

                conn.Open();

                Reader = command.ExecuteReader();


                while (Reader.Read())
                {

                }
                conn.Close();




                // make new log message object and set data, then store in database

                LogMessage LogMessage = new LogMessage();
                LogMessage.personType = "Student";
                LogMessage.action = "Edit";
                LogMessage.date = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "";


                // if statements to check whether course is empty/null and if so then no change has been made and can use old course data
                if (editStudent.course != null)
                {
                    LogMessage.message = "" + LogMessage.personType + " edited, from values (" + logStudent.firstName + ", " + logStudent.lastName + ", " + logStudent.course + ")" +
                    " to values (" + editStudent.firstName + ", " + editStudent.lastName + ", " + editStudent.course + ")";
                }
                else
                {
                    LogMessage.message = "" + LogMessage.personType + " edited, from values (" + logStudent.firstName + ", " + logStudent.lastName + ", " + logStudent.course + ")" +
                    " to values (" + editStudent.firstName + ", " + editStudent.lastName + ", " + logStudent.course + ")";
                }

                String newQuery = "INSERT INTO logs (Person, Action, Date, Message) VALUES " +
                    "('" + LogMessage.personType + "', '" + LogMessage.action + "', '" + LogMessage.date + "', '" + LogMessage.message + "');";

                MySqlCommand cmd = new MySqlCommand(newQuery, conn);

                MySqlDataReader Reader1;

                conn.Open();

                Reader1 = cmd.ExecuteReader();
                conn.Close();





                successMessage = "Successfully updated";
                Response.Redirect("/Students/Index");





            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        }

        
    }
}
