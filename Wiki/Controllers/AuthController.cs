using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Wiki.Models;
using Wiki.Data;
using AiursoftBase.Services;
using AiursoftBase.Attributes;
using System;
using AiursoftBase.Models;
using AiursoftBase.Models.ForApps.AddressModels;
using AiursoftBase;

namespace Wiki.Controllers
{
    [AiurExceptionHandler]
    public class AuthController : AiurController
    {
        public readonly UserManager<WikiUser> _userManager;
        public readonly SignInManager<WikiUser> _signInManager;
        public readonly WikiDbContext _dbContext;

        public AuthController(
            UserManager<WikiUser> userManager,
            SignInManager<WikiUser> signInManager,
            WikiDbContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = _context;
        }

        [AiurForceAuth]
        public IActionResult GoAuth()
        {
            throw new NotImplementedException();
        }


        public async Task<IActionResult> AuthResult(AuthResultAddressModel model)
        {
            if (!User.Identity.IsAuthenticated && ModelState.IsValid)
            {
                await AiursoftBase.AuthProcess.AuthApp(this, model, _userManager, _signInManager);
            }
            return Redirect(model.state);
        }
    }
}