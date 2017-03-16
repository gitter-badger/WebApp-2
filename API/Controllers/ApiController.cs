using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using AiursoftBase.Models;
using Microsoft.AspNetCore.Identity;
using API.Models;
using API.Services;
using Microsoft.Extensions.Logging;
using API.Data;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;
using AiursoftBase.Services;
using AiursoftBase.Services.ToDeveloperServer;
using AiursoftBase.Models.API.ApiViewModels;
using AiursoftBase.Models.API.ApiAddressModels;
using AiursoftBase.Attributes;
using AiursoftBase;
using AiursoftBase.Models.API;

namespace API.Controllers
{
    [AiurExceptionHandler]
    public class ApiController : AiurController
    {
        private readonly UserManager<APIUser> _userManager;
        private readonly SignInManager<APIUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly APIDbContext _dbContext;
        private readonly IStringLocalizer<ApiController> _localizer;

        public ApiController(
            UserManager<APIUser> userManager,
            SignInManager<APIUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            APIDbContext _context,
            IStringLocalizer<ApiController> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ApiController>();
            _dbContext = _context;
            _localizer = localizer;
        }
        [HttpPost]
        public JsonResult SetLang(string culture = "en-US")
        {
            try
            {
                Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            }
            catch (CultureNotFoundException)
            {
                return Json(new AiurProtocal { message = "Not a language.", code = ErrorType.InvalidInput });
            }
            return Json(new AiurProtocal { message = "Changed", code = ErrorType.Success });
        }

        public async Task<JsonResult> ValidateAccessToken(string AccessToken)
        {
            var target = await _dbContext.AccessToken
                .SingleOrDefaultAsync(t => t.Value == AccessToken);
            if (target == null)
            {
                return Json(new ValidateAccessTokenViewModel { code = ErrorType.Unauthorized, message = "We can not validate your access token!" });
            }
            else if (!target.IsAlive)
            {
                return Json(new ValidateAccessTokenViewModel { code = ErrorType.Timeout, message = "Your access token is already Timeout!" });
            }
            else
            {
                return Json(new ValidateAccessTokenViewModel
                {
                    AppId = target.ApplyAppId,
                    code = ErrorType.Success,
                    message = "Successfully validated access token."
                });
            }
        }

        public async Task<IActionResult> AccessToken(AccessTokenAddressModel model)
        {
            var AppValidateState = await APIService.IsValidAppAsync(model.AppId, model.AppSecret);
            if (AppValidateState.code != ErrorType.Success)
            {
                return Json(AppValidateState);
            }
            var newAC = new AccessToken
            {
                ApplyAppId = model.AppId,
                Value = (DateTime.Now.ToString() + HttpContext.GetHashCode().ToString() + model.AppId).GetMD5()
            };
            _dbContext.AccessToken.Add(newAC);
            await _dbContext.SaveChangesAsync();
            return Json(new AccessTokenViewModel
            {
                code = ErrorType.Success,
                message = "Successfully get access token",
                AccessToken = newAC.Value,
                DeadTime = newAC.CreateTime + newAC.AliveTime
            });
        }

        public async Task<IActionResult> AllUserGranted(string AccessToken)
        {
            var target = await _dbContext.AccessToken
                .SingleOrDefaultAsync(t => t.Value == AccessToken);
            if (target == null)
            {
                return Json(new ValidateAccessTokenViewModel { code = ErrorType.Unauthorized, message = "We can not validate your access token!" });
            }
            else if (!target.IsAlive)
            {
                return Json(new ValidateAccessTokenViewModel { code = ErrorType.Timeout, message = "Your access token is already Timeout!" });
            }

            var grants = _dbContext.LocalAppGrant.Include(t=>t.User).Where(t => t.AppID == target.ApplyAppId).Take(200);
            var model = new AllUserGrantedViewModel
            {
                AppId = target.ApplyAppId,
                Grants = new List<AppUserRelation>(),
                code = ErrorType.Success,
                message = "Successfully get all your users"
            };
            foreach (var grant in grants)
            {
                model.Grants.Add(new AppUserRelation
                {
                    Grant = grant,
                    User = new PureUser(grant.User)
                });
            }
            return Json(model);
        }
    }

}