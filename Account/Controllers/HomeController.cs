using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AiursoftBase.Attributes;
using AiursoftBase;

namespace Account.Controllers
{
    [AiurExceptionHandler]
    public class HomeController : AiurController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
