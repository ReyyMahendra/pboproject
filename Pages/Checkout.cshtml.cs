using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class CheckoutModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Photoworks | Checkout";
            // get by id from query string
            var id = Request.Query["paket"];
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
                    var cmd = new NpgsqlCommand("SELECT * FROM packets WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("id", int.Parse(id));
                    var reader = cmd.ExecuteReader();
                    // get data and save to array
                    if(reader.HasRows){
                        // create list
                        while(reader.Read()){
                            // check data null
                            ViewData["Id"] = String.Format("{0}", reader["id"]);
                            ViewData["JenisPaket"] = String.Format("{0}", reader["jenis_paket"]);
                            ViewData["JenisFoto"] = String.Format("{0}", reader["jenis_foto"]);
                            ViewData["UkuranFoto"] = String.Format("{0}", reader["ukuran_foto"]);
                            ViewData["Keterangan"] = String.Format("{0}", reader["keterangan"]);
                            ViewData["Biaya"] = String.Format("{0}", reader["biaya"]);
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
            // get from form
            var id_paket = Request.Form["id_paket"];
            var name = Request.Form["name"];
            var tanggal_booking = Request.Form["date"];
            var biaya = Request.Form["biaya"];
            var session = HttpContext.Session.GetString("username");
            if(session == null){
                Response.Redirect("/Login");
            }else{
                // get user id from database
                var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                try{
                    // insert into database
                    var cmd = new NpgsqlCommand("INSERT INTO orders (username, id_paket, nama_booking, tanggal_booking, dp, status, created_at, updated_at) VALUES (@username, @id_paket, @nama_booking, @tanggal_booking, @dp, 'pending', now(), now())", conn);
                    cmd.Parameters.AddWithValue("username", session);
                    cmd.Parameters.AddWithValue("id_paket", int.Parse(id_paket));
                    cmd.Parameters.AddWithValue("nama_booking", name.ToString());
                    cmd.Parameters.AddWithValue("tanggal_booking", DateOnly.Parse(tanggal_booking));
                    cmd.Parameters.AddWithValue("dp", int.Parse(biaya)/4);
                    cmd.ExecuteNonQuery();
                    // redirect to dashboard
                    Response.Redirect("/MyOrder");
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }
        }   
    }
}
