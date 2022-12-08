using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class MyOrderModel : PageModel
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
                    NpgsqlCommand cmd;
                    if(session == "admin"){
                        cmd = new NpgsqlCommand("SELECT * FROM orders where status != 'done' and status != 'rejected'", conn);
                        var cmd2 = new NpgsqlCommand("SELECT * FROM photographers", conn);
                        var reader2 = cmd2.ExecuteReader();
                        List<string[]> Photographers = new List<string[]>();
                        if(reader2.HasRows){
                            while(reader2.Read()){
                                string[] photographer = new string[4]{
                                    String.Format("{0}", reader2["nama"]),
                                    String.Format("{0}", reader2["status"]),
                                    String.Format("{0}", reader2["jadwal"]),
                                    String.Format("{0}", reader2["id"])
                                };
                                Photographers.Add(photographer);
                            }
                        }
                        reader2.Close();
                        ViewData["Photographers"] = Photographers;
                    }else{
                        cmd = new NpgsqlCommand("SELECT * FROM orders where username = @username and status != 'done' and status != 'rejected'", conn);
                    }
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
