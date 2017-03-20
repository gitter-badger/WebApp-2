﻿using AiursoftBase.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models.OSS.ApiAddressModels
{
    public class CreateBucketAddressModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(5)]
        [NoSpace]
        public string BucketName { get; set; }
        public bool OpenToRead { get; set; }
        public bool OpenToUpload { get; set; }
    }
}
