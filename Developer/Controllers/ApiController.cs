using AiursoftBase;
using AiursoftBase.Attributes;
using AiursoftBase.Models;
using AiursoftBase.Models.Developer.ApiAddressModels;
using AiursoftBase.Models.Developer.ApiViewModels;
using Developer.Data;
using Developer.Models;
using Developer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Developer.Controllers
{
    [AiurExceptionHandler]
    public class ApiController : AiurController
    {
        public readonly UserManager<DeveloperUser> _userManager;
        public readonly SignInManager<DeveloperUser> _signInManager;
        public readonly IEmailSender _emailSender;
        public readonly ISmsSender _smsSender;
        public readonly ILogger _logger;
        public DeveloperDbContext _dbContext;

        public ApiController(
        UserManager<DeveloperUser> userManager,
        SignInManager<DeveloperUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory,
        DeveloperDbContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ApiController>();
            _dbContext = _context;
        }

        public async Task<JsonResult> IsValidApp(IsValidateAppAddressModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AiurProtocal { message = "Wrong input.", code = ErrorType.InvalidInput });
            }
            var _target = await _dbContext.Apps.FindAsync(model.AppId);
            if (_target == null)
            {
                return Json(new AiurProtocal { message = "Target app did not found.", code = ErrorType.NotFound });
            }
            else if (_target.AppSecret != model.AppSecret)
            {
                return Json(new AiurProtocal { message = "Wrong secret.", code = ErrorType.WrongKey });
            }
            else
            {
                return Json(new AiurProtocal { message = "Currect", code = ErrorType.Success });
            }
        }

        public async Task<JsonResult> AppInfo(AppInfoAddressModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AiurProtocal { message = "Wrong input.", code = ErrorType.InvalidInput });
            }
            var target = await _dbContext.Apps.FindAsync(model.AppId);
            if (target == null)
            {
                return Json(new AiurProtocal { message = "Not found.", code = ErrorType.NotFound });
            }
            return Json(new AppInfoViewModel
            {
                AppId = target.AppId,
                message = "Successfully get target app info.",
                code = ErrorType.Success,
                CreaterId = target.CreaterId,
                AppName = target.AppName,
                AppDescription = target.AppDescription,
                AppCategory = target.AppCategory,
                AppPlatform = target.AppPlatform,
                AppCreateTime = target.AppCreateTime,
                EnableOAuth = target.EnableOAuth,
                ForceConfirmation = target.ForceConfirmation,
                ForceInputPassword = target.ForceInputPassword,
                DebugMode = target.DebugMode,
                AppDomain = target.AppDomain,
                PrivacyStatementUrl = target.PrivacyStatementUrl,
                LicenseUrl = target.LicenseUrl,
                AppImageUrl = target.AppIconAddress
            });
        }
    }

}