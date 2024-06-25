using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authorization_02.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet("/account/login")]
        public IActionResult NotAuthenticate()
        {
            return Ok("You're not authenticated!!");
        }

        [HttpGet("/register")]
        public async Task<IActionResult> Register(UserManager<IdentityUser> userManager,
                                                  RoleManager<IdentityRole> roleManager)
        {
            string defaultAdminRole = "admin";
            var adminRole = new IdentityRole(defaultAdminRole);
            var roleCreationResult = await roleManager.CreateAsync(adminRole);

            var user = new IdentityUser("nader");
            var userCreateionResult = await userManager.CreateAsync(user, "1q2w3e");

            var addingToRoleResult = await userManager.AddToRoleAsync(user, defaultAdminRole);

            return Ok(new { roleCreationResult, userCreateionResult, addingToRoleResult });
        }

        [HttpGet("/login")]
        public async Task<IActionResult> Login(SignInManager<IdentityUser> signInManager)
        {
            var signInResult = await signInManager.PasswordSignInAsync("nader", 
                                                                       "1q2w3e", 
                                                                       false, 
                                                                       false);
            return Ok(new { signInResult });
        }
    }
}
