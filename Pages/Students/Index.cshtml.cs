using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace universityManagementSystem.Pages.Students
{
    public class StudentsModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        
        public List<Student> Allstudents = new List<Student>();
        public void OnGet()
        {
            String sort = Request.Query["sortby"];
            String sqlsort = "";

            Debug.WriteLine("hiiiiiiiiiiiii "+sort);

            // Switch for sorting
            switch (sort)
            {
                case "":
                    break;
                case "idasc":
                    sqlsort = " ORDER BY StudentID ASC";
                    break;
                case "iddesc":
                    sqlsort = " ORDER BY StudentID DESC";
                    break;
                case "firstnameASC":
                    sqlsort = " ORDER BY FirstName ASC";
                    break;
                case "firstnameDESC":
                    sqlsort = " ORDER BY FirstName DESC";
                    break;
                case "lastnameASC":
                    sqlsort = " ORDER BY LastName ASC";
                    break;
                case "lasttnameDESC":
                    sqlsort = " ORDER BY LastName DESC";
                    break;
                case "course":
                    sqlsort = " ORDER BY Course";
                    break;
            }

            // Select all students

            try
            {
                String connectionString = "Server=managementsystem.cac8ficgdlsa.us-east-1.rds.amazonaws.com,3306; Database=sys; User Id = admin; Password=vPvdHKV4Ac8zC2uP";

                String Query = "SELECT * FROM students" + sqlsort;

                Debug.WriteLine("HIHIHIHI "+Query);

                MySqlConnection conn = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand(Query, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command.ExecuteReader();


                while (Reader.Read())
                {
                    Student student = new Student();
                    student.Id = Reader.GetInt32(0);
                    student.firstName = Reader.GetString(1);
                    student.lastName = Reader.GetString(2);
                    student.course = Reader.GetString(3);

                    Allstudents.Add(student);

                }
                conn.Close();






            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        
    }
}
