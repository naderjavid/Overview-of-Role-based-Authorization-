using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization_02.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public string Index() => "Welcome to Role-base Authentication App";

        [HttpGet("/secure")]
        [Authorize(Roles = "admin")]
        public string Secure()
        {
            return "You're authorized!!";
        }

        [HttpGet("/secure2")]
        public string Secure2()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                Response.StatusCode = 401;
                return "You're not authenticated!!";
            }
            if (!User.IsInRole("somerole"))
            {
                Response.StatusCode = 403;
                return "You're not authorized!!";
            }

            return "You're logged in!!";
        }
    }
}
