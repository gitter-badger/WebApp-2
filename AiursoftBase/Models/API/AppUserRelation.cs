﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Models.API
{
    public class AppUserRelation
    {
        public AppGrant Grant { get; set; }
        public AiurUserBase User { get; set; }
    }
}
