using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AiursoftBase.Models.API
{
    public class AppGrant
    {
        public virtual string AppID { get; set; }
        public virtual DateTime GrantTime { get; set; } = DateTime.Now;

        public virtual string APIUserId { get; set; }

    }
}
