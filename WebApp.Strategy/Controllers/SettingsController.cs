using BaseProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Settings settings = new();

            if(User.Claims.Where(x=>x.Type==Settings.claimDatabaseType).FirstOrDefault() != null)
            {
                settings.DatabaseType = (EDatabaseType)int.Parse(User.Claims.First(x => x.Type == Settings.claimDatabaseType).Value);
            }
            else
            {
                settings.DatabaseType = settings.GetDeafultDatabaseType;
            }


            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var newClaim = new Claim(Settings.claimDatabaseType, databaseType.ToString());

            var claims = await userManager.GetClaimsAsync(user);

            var hasDatabaseTypeClaim = claims.FirstOrDefault(x=>x.Type==Settings.claimDatabaseType);

            if (hasDatabaseTypeClaim != null)
                await userManager.ReplaceClaimAsync(user, hasDatabaseTypeClaim, newClaim);
            else
                await userManager.AddClaimAsync(user, newClaim);

            await signInManager.SignOutAsync();
            var authenticateResult = await HttpContext.AuthenticateAsync();

            await signInManager.SignInAsync(user, authenticateResult.Properties);

            return RedirectToAction(nameof(Index));


        }
    }
}
