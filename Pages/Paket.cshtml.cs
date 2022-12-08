using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace photoworks.Pages
{
    public class PaketModel : PageModel
    {
        // declare list paket
        public void OnGet()
        {
            ViewData["Title"] = "Photoworks | Daftar Paket";
            // get session
            var session = HttpContext.Session.GetString("username");
            // set message
            if(session != null){
                ViewData["Session"] = session.ToUpper();
            }
            // get from database
            var connString = "Host=localhost;Username=postgres;Password=master;Database=photoworks";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            try{
                var cmd = new NpgsqlCommand("SELECT * FROM packets", conn);
                var reader = cmd.ExecuteReader();
                // get data and save to array

                List<string[]> Pakets = new List<string[]>();
                if(reader.HasRows){
                    // create list
                    while(reader.Read()){
                        // check data null
                        string[] paket = new string[6]{
                            String.Format("{0}", reader["id"]),
                            String.Format("{0}", reader["jenis_paket"]),
                            String.Format("{0}", reader["jenis_foto"]),
                            String.Format("{0}", reader["ukuran_foto"]),
                            String.Format("{0}", reader["keterangan"]),
                            String.Format("{0}", reader["biaya"])
                        };
                        Pakets.Add(paket);
                    }
                }
                // write paket list
                ViewData["Pakets"] = Pakets;
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
