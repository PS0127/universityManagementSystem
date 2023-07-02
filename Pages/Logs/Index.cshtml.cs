using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace universityManagementSystem.Pages.Logs
{
    public class IndexModel : PageModel
    {
        public List<LogMessage> AllLogs = new List<LogMessage>();
        public void OnGet()
        {

            try
            {
                String connectionString = "Server=managementsystem.cac8ficgdlsa.us-east-1.rds.amazonaws.com,3306; Database=sys; User Id = admin; Password=vPvdHKV4Ac8zC2uP";

                String Query = "SELECT * FROM logs";

                MySqlConnection conn = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand(Query, conn);

                MySqlDataReader Reader;

                conn.Open();

                Reader = command.ExecuteReader();


                while (Reader.Read())
                {
                    LogMessage LogMessage = new LogMessage();

                    LogMessage.id = Reader.GetInt32(0);
                    LogMessage.personType = Reader.GetString(1);
                    LogMessage.action = Reader.GetString(2);
                    LogMessage.date = Reader.GetString(3);
                    LogMessage.message = Reader.GetString(4);

                    AllLogs.Add(LogMessage);

                }
                conn.Close();






            }

            catch (Exception ex)
            {
            }
        }
    }
}
