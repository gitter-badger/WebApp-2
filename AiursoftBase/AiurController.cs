using AiursoftBase.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase
{
    public class AiurController : Controller
    {
        public IActionResult Exception(AiurProtocal model)
        {
            return Json(model);
        }
    }
}
