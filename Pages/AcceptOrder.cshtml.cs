using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class AcceptOrderModel : PageModel
    {
        public void OnGet()
        {
            var id = Request.Query["id"].ToString();
            var photographer_id = Request.Query["photographer"].ToString();
            // update order status
            var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            try{
                var cmd = new NpgsqlCommand("UPDATE orders SET status = 'accepted' WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("id", int.Parse(id));
                cmd.ExecuteNonQuery();
                // select orders
                var cmd2 = new NpgsqlCommand("SELECT * FROM orders WHERE id = @id", conn);
                cmd2.Parameters.AddWithValue("id", int.Parse(id));
                // get date
                var reader = cmd2.ExecuteReader();
                string date = "";
                if(reader.HasRows){
                    while(reader.Read()){
                        date = String.Format("{0}", reader["tanggal_booking"]);
                    }
                }
                reader.Close();
                // update photographer jadwal with id
                var cmd3 = new NpgsqlCommand("UPDATE photographers SET jadwal = @date WHERE id = @id", conn);
                cmd3.Parameters.AddWithValue("id", int.Parse(photographer_id));
                cmd3.Parameters.AddWithValue("date", DateTime.Parse(date));
                cmd3.ExecuteNonQuery();
                // redirect to order page
                Response.Redirect("/MyOrder");
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
