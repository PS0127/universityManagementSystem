using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace universityManagementSystem.Pages.Teachers
{
    public class EditModel : PageModel
    {


        public Teacher editTeacher = new Teacher();

        public string errorMessage = "";
        public string successMessage = "";


        public void OnGet()
        {
            String ID = Request.Query["id"];



            try
            {

                String Query = "SELECT * FROM teachers WHERE TeacherID = " + ID;

                MySqlConnection conn = new MySqlConnection(Connection.conn);

                MySqlCommand command = new MySqlCommand(Query, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command.ExecuteReader();


                while (Reader.Read())
                {
                    // Saves database contents into new object for page to show
                    editTeacher.firstName = Reader.GetString(1);
                    editTeacher.lastName = Reader.GetString(2);
                    editTeacher.department = Reader.GetString(3);


                }
                conn.Close();


            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Checks whether student info is null or empty which means no student found
            if (editTeacher.firstName == null || editTeacher.firstName.Length == 0)
            {
                errorMessage = "No student found";
                return;
            }


        }

        public void OnPost()
        {
            String ID = Request.Query["id"];

            editTeacher.firstName = Request.Form["firstname"];
            editTeacher.lastName = Request.Form["lastname"];
            editTeacher.department = Request.Form["department"];


            try
            {


                // get old teacher data

                String Query2 = "SELECT * FROM teachers WHERE TeacherID = " + ID;

                MySqlConnection conn = new MySqlConnection(Connection.conn);

                MySqlCommand command1 = new MySqlCommand(Query2, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command1.ExecuteReader();

                Teacher logTeacher = new Teacher();

                while (Reader.Read())
                {
                    // Saves database contents into new object for page to show
                    logTeacher.firstName = Reader.GetString(1);
                    logTeacher.lastName = Reader.GetString(2);
                    logTeacher.department = Reader.GetString(3);


                }
                conn.Close();



                // Store new teacher data into database
                string Query = "";
                if (editTeacher.department != null)
                {

                    Query = "UPDATE teachers SET" +
                        " FirstName = '" + editTeacher.firstName + "'," +
                        " LastName = '" + editTeacher.lastName + "'," +
                        " Department = '" + editTeacher.department+ "'" +
                        " WHERE TeacherID = " + ID + ";";

                }
                else
                {

                    Query = "UPDATE teachers SET" +
                        " FirstName = '" + editTeacher.firstName + "'," +
                        " LastName = '" + editTeacher.lastName + "'" +
                        " WHERE TeacherID = " + ID + ";";
                }



                MySqlCommand command = new MySqlCommand(Query, conn);

                conn.Open();

                Reader = command.ExecuteReader();


                while (Reader.Read())
                {

                }
                conn.Close();




                // make new log message object and set data, then store in database

                LogMessage LogMessage = new LogMessage();
                LogMessage.personType = "Teacher";
                LogMessage.action = "Edit";
                LogMessage.date = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "";


                // if statements to check whether course is empty/null and if so then no change has been made and can use old department data
                if (editTeacher.department != null)
                {
                    LogMessage.message = "" + LogMessage.personType + " edited, from values (" + logTeacher.firstName + ", " + logTeacher.lastName + ", " + logTeacher.department + ")" +
                    " to values (" + editTeacher.firstName + ", " + editTeacher.lastName + ", " + editTeacher.department + ")";
                }
                else
                {
                    LogMessage.message = "" + LogMessage.personType + " edited, from values (" + logTeacher.firstName + ", " + logTeacher.lastName + ", " + logTeacher.department + ")" +
                    " to values (" + editTeacher.firstName + ", " + editTeacher.lastName + ", " + logTeacher.department + ")";
                }

                String newQuery = "INSERT INTO logs (Person, Action, Date, Message) VALUES " +
                    "('" + LogMessage.personType + "', '" + LogMessage.action + "', '" + LogMessage.date + "', '" + LogMessage.message + "');";

                MySqlCommand cmd = new MySqlCommand(newQuery, conn);

                MySqlDataReader Reader1;

                conn.Open();

                Reader1 = cmd.ExecuteReader();
                conn.Close();





                successMessage = "Successfully updated";
                Response.Redirect("/Teachers/Index");





            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        }
    }
}
