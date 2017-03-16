﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.Extensions.Logging;
using API.Services;
using API.Models;
using Microsoft.AspNetCore.Identity;
using AiursoftBase.Models;
using API.Models.HomeViewModels;
using AiursoftBase;

namespace API.Controllers
{
    public class HomeController : AiurController
    {
        private readonly UserManager<APIUser> _userManager;
        private readonly SignInManager<APIUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly APIDbContext _dbContext;

        public HomeController(
            UserManager<APIUser> userManager,
            SignInManager<APIUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            APIDbContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _dbContext = _context;
        }

        public async Task<IActionResult> Index()
        {
            var cuser = await GetCurrentUserAsync();
            return Json(new IndexViewModel
            {
                Signedin = User.Identity.IsAuthenticated,
                UserId = cuser?.Id,
                ServerTime = DateTime.Now,
                UserName = cuser?.NickName,
                UserImage = cuser?.HeadImgUrl,
                code  = ErrorType.Success,
                message = "Server started successfully!"
            });
        }
        private async Task<APIUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}