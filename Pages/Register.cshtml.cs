using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace photoworks.Pages
{
    public class RegisterModel : PageModel
    {
        public string Error { get; set; } = "";
        public string Message { get; set; } = "";
        public void OnGet()
        {
            ViewData["Title"] = "Photoworks | Register";
        }
        public void OnPost(){
            ViewData["Title"] = "Photoworks | Register";
            var username = Request.Form["username"];
            var password = Request.Form["password"];
            var email = Request.Form["email"];
            var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            try{
                var cmdcheck = new NpgsqlCommand("SELECT * FROM accounts WHERE username = @username", conn);
                cmdcheck.Parameters.AddWithValue("@username", String.Format("{0}", username));
                var reader = cmdcheck.ExecuteReader();
                if(reader.HasRows){
                    // pass error message to page
                    Error = "Username already exist";
                }else{
                    reader.Close();
                    var cmd = new NpgsqlCommand("INSERT INTO accounts (username, password, email,role,created_at,updated_at) VALUES (@username, @password, @email, 'user', now(), now())", conn);
                    cmd.Parameters.AddWithValue("@username", String.Format("{0}", username));
                    cmd.Parameters.AddWithValue("@password", String.Format("{0}", password));
                    cmd.Parameters.AddWithValue("@email", String.Format("{0}", email));
                    cmd.ExecuteNonQuery();
                    Message = "Account created, Please Login";
                }
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
