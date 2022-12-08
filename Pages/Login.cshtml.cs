using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages;
public class LoginModel : PageModel
{
    public string Error { get; set; } = "";
    public void OnGet(){
        ViewData["Title"] = "Photoworks | Login";
    }
    public void OnPost(){
        ViewData["Title"] = "Photoworks | Login";
        var username = Request.Form["username"];
        var password = Request.Form["password"];
        // get data form postgresql
        var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        try{
            var cmd = new NpgsqlCommand("SELECT * FROM accounts WHERE username = @username AND password = @password", conn);
            cmd.Parameters.AddWithValue("@username", String.Format("{0}", username));
            cmd.Parameters.AddWithValue("@password", String.Format("{0}", password));

            var reader = cmd.ExecuteReader();

            // check if user exist
            if(reader.HasRows){
                // set session
                HttpContext.Session.SetString("username", String.Format("{0}", username));
                // redirect to home page
                Response.Redirect("/Landing");
            }else{
                // pass error message to page
                Error = "Invalid username or password";
            }
            reader.Close();
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
}
