using AiursoftBase.Models;
using AiursoftBase.Models.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AppGrant : IAppGrant
    {
        [JsonIgnore]
        public int AppGrantId { get; set; }
        public string AppID { get; set; }
        public DateTime GrantTime { get; set; }// = DateTime.Now;
        public string APIUserId { get; set; }
        [JsonIgnore]
        public APIUser User { get; set; }

        public AiurUserBase UserInfo => User;
    }
}
