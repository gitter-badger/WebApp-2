﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models.OSS.ApiViewModels
{
    public class UploadFileViewModel : AiurProtocal
    {
        public int FileKey { get; set; }
        public string Path { get; set; }
    }
}
