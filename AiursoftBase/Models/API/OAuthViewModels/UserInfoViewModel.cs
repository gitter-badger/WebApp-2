﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models.API.OAuthViewModels
{
    public class UserInfoViewModel : AiurProtocal
    {
        public virtual AiurUserBase User { get; set; }
    }
}
