using AiursoftBase.Models.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class LocalAppGrant : AppGrant
    {
        public virtual int LocalAppGrantId { get; set; }

        [ForeignKey(nameof(APIUserId))]
        [JsonIgnore]
        public virtual APIUser User { get; set; }
    }
}
