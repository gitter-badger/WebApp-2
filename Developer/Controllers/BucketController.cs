﻿using AiursoftBase;
using AiursoftBase.Attributes;
using AiursoftBase.Exceptions;
using AiursoftBase.Models;
using AiursoftBase.Models.OSS;
using AiursoftBase.Services.ToOSSServer;
using Developer.Data;
using Developer.Models;
using Developer.Models.BucketViewModels;
using Developer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Controllers
{
    [AiurForceAuth]
    [AiurExceptionHandler]
    public class BucketController : AiurController
    {
        private readonly UserManager<DeveloperUser> _userManager;
        private readonly SignInManager<DeveloperUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly DeveloperDbContext _dbContext;

        public BucketController(
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
            _logger = loggerFactory.CreateLogger<BucketController>();
            _dbContext = _context;
        }

        public async Task<IActionResult> Index()
        {
            var cuser = await GetCurrentUserAsync();
            var allBuckets = new List<Bucket>();
            foreach (var app in cuser.MyApps)
            {
                var appInfo = await ApiService.ViewMyBucketsAsync(await AppsContainer.AccessToken(app.AppId, app.AppSecret)());
                allBuckets.AddRange(appInfo.Buckets);
            }
            var model = new IndexViewModel(cuser)
            {
                AllBuckets = allBuckets
            };
            return View(model);
        }

        public async Task<IActionResult> CreateBucket(string id)//AppId
        {
            var cuser = await GetCurrentUserAsync();
            var viewModel = new CreateBucketViewModel(this, cuser)
            {
                AppId = id,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBucket([FromForm]CreateBucketViewModel model)
        {
            var cuser = await GetCurrentUserAsync();
            if (!ModelState.IsValid)
            {
                model.ModelStateValid = false;
                model.Recover(this, cuser);
                return View(model);
            }
            var app = await _dbContext.Apps.FindAsync(model.AppId);
            if (app == null)
            {
                return NotFound();
            }
            try
            {
                var token = AppsContainer.AccessToken(app.AppId, app.AppSecret);
                var result = await ApiService.CreateBucketAsync(await token(), model.NewBucketName, model.OpenToRead, model.OpenToUpload);
                return RedirectToAction(nameof(AppsController.ViewApp), "Apps", new { id = app.AppId, JustHaveUpdated = true });
            }
            catch (AiurUnexceptedResponse e)
            {
                ModelState.AddModelError(string.Empty, e.Response.message);
                model.ModelStateValid = false;
                model.Recover(this, cuser);
                return View(model);
            }
        }

        public async Task<IActionResult> EditBucket(int id)//BucketId
        {
            var cuser = await GetCurrentUserAsync();
            var bucket = await ApiService.ViewBucketDetailAsync(id);
            var model = new EditBucketViewModel(cuser, bucket)
            {
                AppId = bucket.BelongingAppId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBucket(EditBucketViewModel model)
        {
            var cuser = await GetCurrentUserAsync();
            if (!ModelState.IsValid)
            {
                model.ModelStateValid = false;
                model.Recover(cuser, 1);
                return View(model);
            }
            try
            {
                var app = await _dbContext.Apps.FindAsync(model.AppId);
                var token = AppsContainer.AccessToken(app.AppId, app.AppSecret);
                var bucket = await ApiService.ViewBucketDetailAsync(model.BucketId);
                if (bucket.BelongingAppId != app.AppId || app.CreaterId != cuser.Id) return Unauthorized();

                await ApiService.EditBucketAsync(await token(), model.BucketId, model.NewBucketName, model.OpenToRead, model.OpenToUpload);
                return RedirectToAction(nameof(AppsController.ViewApp), "Apps", new { id = model.AppId, JustHaveUpdated = true });
            }
            catch (AiurUnexceptedResponse e)
            {
                ModelState.AddModelError(string.Empty, e.Response.message);
                model.ModelStateValid = false;
                model.Recover(cuser, 1);
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteBucket(int Id)//BucketId
        {
            var cuser = await GetCurrentUserAsync();
            var bucket = await ApiService.ViewBucketDetailAsync(Id);
            var model = new DeleteBucketViewModel(cuser)
            {
                BucketName = bucket.BucketName,
                FilesCount = bucket.FileCount,
                AppId = bucket.BelongingAppId,
                BucketId = bucket.BucketId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBucket([FromForm]DeleteBucketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var app = await _dbContext.Apps.FindAsync(model.AppId);
                var cuser = await GetCurrentUserAsync();
                var token = AppsContainer.AccessToken(app.AppId, app.AppSecret);
                var bucket = await ApiService.ViewBucketDetailAsync(model.BucketId);
                if (bucket.BelongingAppId != app.AppId || app.CreaterId != cuser.Id)
                {
                    return Unauthorized();
                }
                await ApiService.DeleteBucketAsync(await token(), model.BucketId);
                return RedirectToAction(nameof(AppsController.ViewApp), "Apps", new { id = model.AppId });
            }
            return View(model);
        }

        private async Task<DeveloperUser> GetCurrentUserAsync()
        {
            return await _dbContext.Users.Include(t => t.MyApps).SingleOrDefaultAsync(t => t.UserName == User.Identity.Name);
        }
    }
}
