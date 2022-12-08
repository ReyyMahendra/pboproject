using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class PaymentsModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Photoworks | Checkout";
            // get by id from query string
            var id = Request.Query["id"];
            // get session
            var session = HttpContext.Session.GetString("username");
            // set message
            if(session != null){
                ViewData["Session"] = session.ToUpper();
                // get order by id
                var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                try{
                    var cmd = new NpgsqlCommand("SELECT * FROM orders where id = @id", conn);
                    cmd.Parameters.AddWithValue("id", int.Parse(id));
                    var reader = cmd.ExecuteReader();
                    // get data and save to array
                    if(reader.HasRows){
                        // create list
                        while(reader.Read()){
                            // check data null
                            ViewData["Id"] = String.Format("{0}", reader["id"]);
                            ViewData["IdPaket"] = String.Format("{0}", reader["id_paket"]);
                            ViewData["NamaBooking"] = String.Format("{0}", reader["nama_booking"]);
                            ViewData["TanggalBooking"] = String.Format("{0}", reader["tanggal_booking"]);
                            ViewData["Dp"] = String.Format("{0}", reader["dp"]);
                            ViewData["Status"] = String.Format("{0}", reader["status"]);
                            ViewData["CreatedAt"] = String.Format("{0}", reader["created_at"]);
                        }
                    }
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }else{
                Response.Redirect("/Login");
            }
        }
        public void OnPost(){
            // get session
            var session = HttpContext.Session.GetString("username");
            // set message
            if(session != null){
                ViewData["Session"] = session.ToUpper();
                // get data from form
                var id = Request.Form["id"];
                var metode_pembayaran = Request.Form["payment"];
                // get order by id
                var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                try
                {
                    var cmd3 = new NpgsqlCommand("SELECT * FROM orders where id = @id", conn);
                    cmd3.Parameters.AddWithValue("id", int.Parse(id));
                    var reader = cmd3.ExecuteReader();
                    // get data and save to array
                    if(reader.HasRows){
                        // create list
                        while(reader.Read()){
                            // check data null
                            ViewData["Id"] = String.Format("{0}", reader["id"]);
                            ViewData["IdPaket"] = String.Format("{0}", reader["id_paket"]);
                            ViewData["NamaBooking"] = String.Format("{0}", reader["nama_booking"]);
                            ViewData["TanggalBooking"] = String.Format("{0}", reader["tanggal_booking"]);
                            ViewData["Dp"] = String.Format("{0}", reader["dp"]);
                            ViewData["Status"] = String.Format("{0}", reader["status"]);
                            ViewData["CreatedAt"] = String.Format("{0}", reader["created_at"]);
                        }
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                var total = int.Parse(ViewData["Dp"].ToString());
                try{
                    // insert to transactions
                    var cmd = new NpgsqlCommand("INSERT INTO transactions (orders_id, total, metode_pembayaran, status, created_at) VALUES (@orders_id, @total, @metode_pembayaran, @status, NOW())", conn);
                    cmd.Parameters.AddWithValue("orders_id", int.Parse(id));
                    cmd.Parameters.AddWithValue("total", total);
                    cmd.Parameters.AddWithValue("metode_pembayaran", metode_pembayaran.ToString());
                    cmd.Parameters.AddWithValue("status", "pending");
                    cmd.Parameters.AddWithValue("id", int.Parse(id));
                    cmd.ExecuteNonQuery();
                    try
                    {
                        // update status order
                        var cmd2 = new NpgsqlCommand("UPDATE orders SET status = @status WHERE id = @id", conn);
                        cmd2.Parameters.AddWithValue("status", "paid");
                        cmd2.Parameters.AddWithValue("id", int.Parse(id));
                        cmd2.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {  
                        Console.WriteLine(e.Message);
                    }
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }else{
                Response.Redirect("/Login");
            }
        }
    }
}
