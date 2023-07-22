using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace universityManagementSystem.Pages.Teachers
{
    public class TeachersModel : PageModel
    {

        public string errorMessage = "";
        public string successMessage = "";

        public List<Teacher> Allteachers = new List<Teacher>();

        public void OnGet()
        {

            // Select all teachers

            try
            {

                String Query = "SELECT * FROM teachers";

                MySqlConnection conn = new MySqlConnection(Connection.conn);

                MySqlCommand command = new MySqlCommand(Query, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command.ExecuteReader();


                while (Reader.Read())
                {
                    Teacher teacher = new Teacher();
                    teacher.Id = Reader.GetInt32(0);
                    teacher.firstName = Reader.GetString(1);
                    teacher.lastName = Reader.GetString(2);
                    teacher.department = Reader.GetString(3);

                    Allteachers.Add(teacher);

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
