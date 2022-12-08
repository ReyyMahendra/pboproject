using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class OrderDetailsModel : PageModel
    {
        public string Message { get; set; } = "";
        public void OnGet()
        {
            ViewData["Title"] = "Photoworks | Detail Order";
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
                        reader.Close();
                        if(ViewData["Status"].ToString() != "pending"){
                            try
                            {
                                var cmd3 = new NpgsqlCommand("SELECT * FROM transactions where orders_id = @id", conn);
                                cmd3.Parameters.AddWithValue("id", int.Parse(id));
                                var reader2 = cmd3.ExecuteReader();
                                // get data and save to array
                                if(reader2.HasRows){
                                    // create list
                                    while(reader2.Read()){
                                        // check data null
                                        ViewData["IdTransaksi"] = String.Format("{0}", reader2["id"]);
                                        ViewData["IdOrder"] = String.Format("{0}", reader2["orders_id"]);
                                        ViewData["Total"] = String.Format("{0}", reader2["total"]);
                                        ViewData["Metode"] = String.Format("{0}", reader2["metode_pembayaran"]);
                                        ViewData["StatusTransaksi"] = String.Format("{0}", reader2["status"]);
                                    }
                                }
                                
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
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
                var namaBooking = Request.Form["nama"];
                var tanggalBooking = Request.Form["tanggal"];
                // get order by id
                var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                try{
                    var cmd = new NpgsqlCommand("UPDATE orders SET nama_booking = @nama_booking, tanggal_booking = @tanggal_booking WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("id", int.Parse(id));
                    cmd.Parameters.AddWithValue("nama_booking", namaBooking.ToString());
                    cmd.Parameters.AddWithValue("tanggal_booking", DateTime.Parse(tanggalBooking));
                    cmd.ExecuteNonQuery();
                    // redirect to order details
                    var cmd2 = new NpgsqlCommand("SELECT * FROM orders where id = @id", conn);
                    cmd2.Parameters.AddWithValue("id", int.Parse(id));
                    var reader = cmd2.ExecuteReader();
                    // get data and save to array
                    if(reader.HasRows){
                        try{
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
                        }catch(Exception e){
                            Console.WriteLine(e.Message);
                        }
                    }
                    Message = "Pesanan berhasil diubah";    
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }else{
                Response.Redirect("/Login");
            }
        }
    }
}