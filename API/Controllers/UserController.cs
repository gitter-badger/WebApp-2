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
using AiursoftBase.Models.API.UserAddressModels;

namespace API.Controllers
{
    [AiurExceptionHandler]
    public class UserController : AiurController
    {
        private readonly UserManager<APIUser> _userManager;
        private readonly SignInManager<APIUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly APIDbContext _dbContext;
        private readonly IStringLocalizer<ApiController> _localizer;

        public UserController(
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

        public async Task<JsonResult> ChangeProfile(ChangeProfileAddressModel model)
        {
            var target = await _dbContext
                .AccessToken
                .SingleOrDefaultAsync(t => t.Value == model.AccessToken);

            if (target == null)
            {
                return Json(new ValidateAccessTokenViewModel { code = ErrorType.Unauthorized, message = "We can not validate your access token!" });
            }
            else if (!target.IsAlive)
            {
                return Json(new ValidateAccessTokenViewModel { code = ErrorType.Timeout, message = "Your access token is already Timeout!" });
            }

        }
    }
}