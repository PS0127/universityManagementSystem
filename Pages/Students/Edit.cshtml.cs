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
            // UPDATE Customers
            //SET ContactName = 'Alfred Schmidt', City = 'Frankfurt'
            //WHERE CustomerID = 1;

            editStudent.firstName = Request.Form["firstname"];
            editStudent.lastName = Request.Form["lastname"];
            editStudent.course = Request.Form["course"];


            try
            {
                String connectionString = "Server=managementsystem.cac8ficgdlsa.us-east-1.rds.amazonaws.com,3306; Database=sys; User Id = admin; Password=vPvdHKV4Ac8zC2uP";
                
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

                MySqlConnection conn = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand(Query, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command.ExecuteReader();


                while (Reader.Read())
                {

                }
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
