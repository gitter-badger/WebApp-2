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
    public class AppGrant : Grant
    {
        [JsonIgnore]
        public int AppGrantId { get; set; }
        [JsonIgnore]
        public APIUser User { get; set; }

        public override AiurUserBase UserInfo => User;
    }
}
