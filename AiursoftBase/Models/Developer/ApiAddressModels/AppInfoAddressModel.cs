﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models.Developer.ApiAddressModels
{
    public class AppInfoAddressModel
    {
        [Required]
        public virtual string AppId { get; set; }
    }
}
