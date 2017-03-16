using AiursoftBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiursoftBase.Attributes;
using Microsoft.AspNetCore.Mvc;
using Account.Models;
using Account.Data;
using Microsoft.EntityFrameworkCore;
using Account.Models.AccountViewModels;

namespace Account.Controllers
{
    [AiurExceptionHandler]
    [AiurForceAuth]
    public class AccountController : AiurController
    {
        private readonly AccountDbContext _dbContext;
        public AccountController(AccountDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var model = new IndexViewModel(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            var cuser = await GetCurrentUserAsync();
            if(!ModelState.IsValid)
            {
                model.ModelStateValid = false;
                model.Recover(cuser);
                return View(model);
            }
            //Update user profile...
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Avatar()
        {
            var user = await GetCurrentUserAsync();
            var model = new AvatarViewModel(user);
            return View(model);
        }

        private async Task<AccountUser> GetCurrentUserAsync()
        {
            return await _dbContext.Users.SingleOrDefaultAsync(t => t.UserName == User.Identity.Name);
        }
    }
}
