using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class DeleteOrderModel : PageModel
    {
        public void OnGet()
        {
            var id = Request.Query["id"].ToString();
            // delete order by id
            var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            try{
                var cmd = new NpgsqlCommand("DELETE FROM orders WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("id", int.Parse(id));
                cmd.ExecuteNonQuery();
                var cmd2 = new NpgsqlCommand("DELETE FROM transactions WHERE orders_id = @id", conn);
                cmd2.Parameters.AddWithValue("id", int.Parse(id));
                cmd2.ExecuteNonQuery();
                // redirect to order page
                Response.Redirect("/MyOrder");
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
