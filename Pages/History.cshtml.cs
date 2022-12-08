using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class HistoryModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Photoworks | My Order";
            // get session
            var session = HttpContext.Session.GetString("username");
            // set message
            if(session != null){
                ViewData["Session"] = session.ToUpper();
                // get from database
                var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                try{
                    // where username and status is not done
                    var cmd = new NpgsqlCommand("SELECT * FROM orders where username = @username and status = 'done' or status = 'rejected'", conn);
                    cmd.Parameters.AddWithValue("username", session);
                    var reader = cmd.ExecuteReader();
                    // get data and save to array

                    List<string[]> Orders = new List<string[]>();
                    if(reader.HasRows){
                        // create list
                        while(reader.Read()){
                            // check data null
                            string[] order = new string[7]{
                                String.Format("{0}", reader["id"]),
                                String.Format("{0}", reader["id_paket"]),
                                String.Format("{0}", reader["nama_booking"]),
                                String.Format("{0}", reader["tanggal_booking"]),
                                String.Format("{0}", reader["dp"]),
                                String.Format("{0}", reader["status"]),
                                String.Format("{0}", reader["created_at"])
                            };
                            Orders.Add(order);
                        }
                    }
                    // write paket list
                    ViewData["Orders"] = Orders;
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }else{
                Response.Redirect("/Login");
            }
        }
    }
}
