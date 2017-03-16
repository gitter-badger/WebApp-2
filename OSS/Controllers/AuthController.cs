using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using OSS.Models;
using OSS.Data;
using AiursoftBase.Attributes;
using System;
using AiursoftBase.Models.ForApps.AddressModels;
using AiursoftBase;

namespace OSS.Controllers
{
    [AiurExceptionHandler]
    public class AuthController : AiurController
    {
        public readonly UserManager<OSSUser> _userManager;
        public readonly SignInManager<OSSUser> _signInManager;
        public readonly OSSDbContext _dbContext;

        public AuthController(
            UserManager<OSSUser> userManager,
            SignInManager<OSSUser> signInManager,
            OSSDbContext _context)
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
                await AuthProcess.AuthApp(this, model, _userManager, _signInManager);
            }
            return Redirect(model.state);
        }
    }
}