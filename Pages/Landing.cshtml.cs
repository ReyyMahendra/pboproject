using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace photoworks.Pages
{
    public class LandingModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Photoworks | Landing";
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
        }
    }
}
