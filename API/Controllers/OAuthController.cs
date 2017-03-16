using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using API.Models.OAuthViewModels;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AiursoftBase.Services;
using AiursoftBase.Models;
using System.Linq;
using Microsoft.Extensions.Localization;
using AiursoftBase.Services.ToDeveloperServer;
using AiursoftBase.Models.API.OAuthAddressModels;
using AiursoftBase.Models.API.OAuthViewModels;
using AiursoftBase.Models.ForApps.AddressModels;
using AiursoftBase;

namespace API.Controllers
{
    public class OAuthController : AiurController
    {
        private readonly UserManager<APIUser> _userManager;
        private readonly SignInManager<APIUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly APIDbContext _dbContext;
        private readonly IStringLocalizer<OAuthController> _localizer;

        public OAuthController(
            UserManager<APIUser> userManager,
            SignInManager<APIUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            APIDbContext _context,
            IStringLocalizer<OAuthController> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<OAuthController>();
            _dbContext = _context;
            _localizer = localizer;
        }

        //http://localhost:62631/oauth/authorize?appid=29bf5250a6d93d47b6164ac2821d5009&redirect_uri=http%3A%2F%2Flocalhost%3A55771%2FAuth%2FAuthResult&response_type=code&scope=snsapi_base&state=http%3A%2F%2Flocalhost%3A55771%2FAuth%2FGoAuth#aiursoft_redirect
        public async Task<IActionResult> Authorize(AuthorizeAddressModel model)
        {
            var capp = await APIService.AppInfoAsync(model.appid);
            var url = new Uri(model.redirect_uri);
            if (url.Host != capp.AppDomain && capp.DebugMode == false)
            {
                ModelState.AddModelError(string.Empty,"Redirect uri did not work in the valid domain!");
                return View();
            }
            var cuser = await GetCurrentUserAsync();
            if (cuser != null && capp.ForceInputPassword == false && model.ForceSignInLocally == false)
            {
                return await FinishAuth(model.Convert(cuser.Email), capp.ForceConfirmation);
            }
            if (ModelState.IsValid)
            {
                var viewModel = new AuthorizeViewModel(model.redirect_uri, model.state, model.appid, model.scope, model.response_type, capp.AppName, capp.AppImageUrl);
                return View(viewModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authorize(AuthorizeViewModel model)
        {
            var capp = await APIService.AppInfoAsync(model.AppId);
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return await FinishAuth(model, capp.ForceConfirmation);
                }
                else if (result.RequiresTwoFactor)
                {
                    throw new NotImplementedException();
                }
                else if (result.IsLockedOut)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            model.Recover(capp.AppName, capp.AppImageUrl);
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> AuthorizeConfirm(AuthorizeConfirmAddressModel model)
        {
            var cuser = await GetCurrentUserAsync();
            if (ModelState.IsValid && cuser != null)
            {
                var capp = await APIService.AppInfoAsync(model.AppId);
                var viewModel = new AuthorizeConfirmViewModel
                {
                    AppName = capp.AppName,
                    UserNickName = cuser.NickName,
                    AppId = model.AppId,
                    ToRedirect = model.ToRedirect,
                    State = model.State,
                    Scope = model.Scope,
                    ResponseType = model.ResponseType,
                    UserIcon = cuser.HeadImgUrl,
                };
                return View(viewModel);
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AuthorizeConfirm(AuthorizeConfirmViewModel model)
        {
            var cuser = await GetCurrentUserAsync();
            if (ModelState.IsValid && cuser != null)
            {
                model.Email = cuser.Email;
                await cuser.GrantTargetApp(_dbContext, model.AppId);
                return await FinishAuth(model);
            }
            return View(model);
        }

        public IActionResult Register(AuthorizeAddressModel model)
        {
            if (ModelState.IsValid)
            {
                var viewModel = new RegisterViewModel()
                {
                    ToRedirect = model.redirect_uri,
                    State = model.state,
                    AppId = model.appid,
                    Scope = model.scope,
                    ResponseType = model.response_type
                };
                return View(viewModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new APIUser { UserName = model.Email, Email = model.Email, NickName = model.Email.Split('@')[0] };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return await FinishAuth(model);
                }
                AddErrors(result);
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Json(new AiurProtocal { message = "Successfully signed out!", code = ErrorType.Success });
        }

        public async Task<IActionResult> CodeToOpenId(CodeToOpenIdAddressModel model)
        {
            var AccessToken = await _dbContext.AccessToken.SingleOrDefaultAsync(t => t.Value == model.AccessToken);
            if (AccessToken == null)
            {
                return Json(new AiurProtocal { message = "Not a valid access token!", code = ErrorType.Unauthorized });
            }

            var targetPack = await _dbContext
                .OAuthPack
                .Where(t => t.IsUsed == false)
                .SingleOrDefaultAsync(t => t.Code == model.Code);

            if (targetPack == null)
            {
                return Json(new AiurProtocal { message = "Invalid Code.", code = ErrorType.WrongKey });
            }
            if (targetPack.ApplyAppId != AccessToken.ApplyAppId)
            {
                return Json(new AiurProtocal { message = "The app granted code is not the app granting access token!", code = ErrorType.Unauthorized });
            }

            targetPack.IsUsed = true;
            var viewModel = new CodeToOpenIdViewModel
            {
                openid = targetPack.UserId,
                scope = "scope",
                message = "Successfully get user openid",
                code = ErrorType.Success
            };
            return Json(viewModel);
        }

        public async Task<IActionResult> UserInfo(UserInfoAddressModel model)
        {
            var target = await _dbContext
                .AccessToken
                .SingleOrDefaultAsync(t => t.Value == model.access_token);

            if (target == null)
            {
                return Json(new AiurProtocal { message = "Invalid Access Token!", code = ErrorType.WrongKey });
            }
            else if (!target.IsAlive)
            {
                return Json(new AiurProtocal { message = "Access Token is timeout!", code = ErrorType.Timeout });
            }
            var cuser = await _userManager.FindByIdAsync(model.openid);
            var viewModel = new UserInfoViewModel
            {
                code = 0,
                message = "Successfully get target user info.",
                User = new PureUser(cuser)
            };
            return Json(viewModel);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task<APIUser> GetCurrentUserAsync()
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(t => t.UserName == User.Identity.Name);
        }

        private async Task<APIUser> GetCurrentUserAsync(string email)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(t => t.Email == email);
        }

        private async Task<IActionResult> FinishAuth(IOAuthInfo model, bool forceGrant = false)
        {
            var cuser = await GetCurrentUserAsync(model.Email);
            if (await cuser.HasAuthorizedApp(_dbContext, model.AppId) && forceGrant == false)
            {
                var pack = await cuser.GeneratePack(_dbContext, model.AppId);
                var url = new AiurUrl(model.GetRegexRedirectUrl(), new AuthResultAddressModel
                {
                    code = pack.Code,
                    state = model.State
                });
                return Redirect(url);
            }
            else
            {
                return RedirectToAction(nameof(AuthorizeConfirm), new AuthorizeConfirmAddressModel
                {
                    AppId = model.AppId,
                    State = model.State,
                    ToRedirect = model.ToRedirect,
                    Scope = model.Scope,
                    ResponseType = model.ResponseType
                });
            }
        }

        private RedirectResult Redirect(AiurUrl url)
        {
            return Redirect(url.ToString());
        }
    }
}
