﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Models.API.ApiViewModels
{
    public class AllUserGrantedViewModel : AiurProtocal
    {
        public string AppId { get; set; }
        public List<Grant> Grants { get; set; }
    }
}
