using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace universityManagementSystem.Pages.Students
{
    public class StudentsModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        
        public List<Student> Allstudents = new List<Student>();
        public void OnGet()
        {
            

            // Select all students

            try
            {
                String connectionString = "Server=managementsystem.cac8ficgdlsa.us-east-1.rds.amazonaws.com,3306; Database=sys; User Id = admin; Password=vPvdHKV4Ac8zC2uP";

                String Query = "SELECT * FROM students";

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
