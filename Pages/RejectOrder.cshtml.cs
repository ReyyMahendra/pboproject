using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class RejectOrderModel : PageModel
    {
        public void OnGet()
        {
            var id = Request.Query["id"].ToString();
            // update order status
            var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            try{
                var cmd = new NpgsqlCommand("UPDATE orders SET status = 'rejected' WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("id", int.Parse(id));
                cmd.ExecuteNonQuery();
                // redirect to order page
                Response.Redirect("/MyOrder");
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
