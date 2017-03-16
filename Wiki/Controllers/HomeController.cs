using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Wiki.Models;
using Wiki.Models.HomeViewModels;
using Wiki.Data;
using Microsoft.EntityFrameworkCore;

namespace Wiki.Controllers
{
    public class HomeController : Controller
    {
        public readonly SignInManager<WikiUser> _signInManager;
        public readonly ILogger _logger;
        public readonly WikiDbContext _dbContext;

        public HomeController(
            SignInManager<WikiUser> signInManager,
            ILoggerFactory loggerFactory,
            WikiDbContext _context)
        {
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<HomeController>();
            this._dbContext = _context;
        }

        public async Task<IActionResult> Index(string id)//Title
        {
            if (string.IsNullOrEmpty(id))
            {
                id = (await _dbContext.Article.FirstAsync()).ArticleTitle;
            }
            var article = await _dbContext.Article.SingleOrDefaultAsync(t => t.ArticleTitle == id);
            var model = new IndexViewModel
            {
                Articles = _dbContext.Article,
                CurrentArticleId = article.ArticleId,
                Content = article?.ArticleContent
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
