using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages;

public class AccountModel : PageModel
{
    private readonly ILogger<AccountModel> _logger;

    public AccountModel(ILogger<AccountModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        ViewData["Title"] = "Photoworks | Account";
        // get session
        var session = HttpContext.Session.GetString("username");
        // set message
        if(session != null){
            ViewData["isView"] = "true";
            ViewData["Message"] = "Welcome " + session + "!";
            ViewData["Session"] = session.ToUpper();
        }else{
            // redirect to login page
            Response.Redirect("/Login");
        }
        // get data form postgresql
        var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        try{
            var cmd = new NpgsqlCommand("SELECT * FROM accounts WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@username", String.Format("{0}", session));

            var reader = cmd.ExecuteReader();
            // check if user exist
            if(reader.HasRows){
                while(reader.Read()){
                    // check data null
                    if(reader["username"] != DBNull.Value){
                        ViewData["Username"] = reader["username"];
                    }
                    if(reader["email"] != DBNull.Value){
                        ViewData["Email"] = reader["email"];
                    }
                    if(reader["password"] != DBNull.Value){
                        ViewData["Password"] = reader["password"];
                    }
                    if(reader["nomor_hp"] != DBNull.Value){
                        ViewData["Phone"] = reader["nomor_hp"];
                    }
                    if(reader["alamat"] != DBNull.Value){
                        ViewData["Address"] = reader["alamat"];
                    }
                    if(reader["name"] != DBNull.Value){
                        ViewData["Nama"] = reader["name"];
                    }
                }
            }else{
                // pass error message to page
                ViewData["Error"] = "Invalid username or password";
            }
            reader.Close();
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
    public string Message { get; set; } = "";
    public void OnPost(){
        ViewData["isView"] = "true";
        ViewData["Title"] = "Photoworks | Account";
        // get session
        var session = HttpContext.Session.GetString("username");
        // set message
        if(session != null){
            ViewData["Message"] = "Welcome " + session + "!";
            ViewData["Session"] = session.ToUpper();
        }else{
            // redirect to login page
            Response.Redirect("/Login");
        }
        // get data form postgresql
        var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        try{
            // insert
            var cmd = new NpgsqlCommand("UPDATE accounts SET email = @email, password = @password, nomor_hp = @nomor_hp, alamat = @alamat, name = @name, updated_at = now() WHERE username = @username", conn);
            if(Request.Form["email"] != DBNull.Value){
                cmd.Parameters.AddWithValue("@email", String.Format("{0}", Request.Form["email"]));
            }
            if(Request.Form["password"] != DBNull.Value){
                cmd.Parameters.AddWithValue("@password", String.Format("{0}", Request.Form["password"]));
            }
            if(Request.Form["phone"] != DBNull.Value){
                cmd.Parameters.AddWithValue("@nomor_hp", String.Format("{0}", Request.Form["phone"]));
            }
            if(Request.Form["address"] != DBNull.Value){
                cmd.Parameters.AddWithValue("@alamat", String.Format("{0}", Request.Form["address"]));
            }
            if(Request.Form["name"] != DBNull.Value){
                cmd.Parameters.AddWithValue("@name", String.Format("{0}", Request.Form["name"]));
            }
            cmd.Parameters.AddWithValue("@username", String.Format("{0}", session));
            cmd.ExecuteNonQuery();

            var cmd2 = new NpgsqlCommand("SELECT * FROM accounts WHERE username = @username", conn);
            cmd2.Parameters.AddWithValue("@username", String.Format("{0}", session));

            var reader = cmd2.ExecuteReader();
            // check if user exist
            if(reader.HasRows){
                while(reader.Read()){
                    // check data null
                    if(reader["username"] != DBNull.Value){
                        ViewData["Username"] = reader["username"];
                    }
                    if(reader["email"] != DBNull.Value){
                        ViewData["Email"] = reader["email"];
                    }
                    if(reader["password"] != DBNull.Value){
                        ViewData["Password"] = reader["password"];
                    }
                    if(reader["nomor_hp"] != DBNull.Value){
                        ViewData["Phone"] = reader["nomor_hp"];
                    }
                    if(reader["alamat"] != DBNull.Value){
                        ViewData["Address"] = reader["alamat"];
                    }
                    if(reader["name"] != DBNull.Value){
                        ViewData["Nama"] = reader["name"];
                    }
                }
                Message = "Account Updated";
            }else{
                // pass error message to page
                ViewData["Error"] = "Invalid username or password";
            }
            reader.Close();
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
}

