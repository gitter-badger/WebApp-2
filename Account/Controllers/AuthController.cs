using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Account.Models;
using Account.Data;
using AiursoftBase.Services;
using AiursoftBase.Attributes;
using System;
using AiursoftBase.Models;
using AiursoftBase.Models.ForApps.AddressModels;
using AiursoftBase;

namespace Account.Controllers
{
    [AiurExceptionHandler]
    public class AuthController : AiurController
    {
        public readonly UserManager<AccountUser> _userManager;
        public readonly SignInManager<AccountUser> _signInManager;
        public readonly AccountDbContext _dbContext;

        public AuthController(
            UserManager<AccountUser> userManager,
            SignInManager<AccountUser> signInManager,
            AccountDbContext _context)
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