using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AiursoftBase.Models.API.OAuthViewModels;
using Newtonsoft.Json;

namespace AiursoftBase.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AiurUserBase : IdentityUser
    {
        public AiurUserBase() { }
        public AiurUserBase(UserInfoViewModel model)
        {
            this.Update(model);
        }
        public void Update(UserInfoViewModel model)
        {
            this.NickName = model.User.NickName;
            this.Sex = model.User.Sex;
            this.PreferedLanguage = model.User.PreferedLanguage;
            this.HeadImgUrl = model.User.HeadImgUrl;
            this.AccountCreateTime = model.User.AccountCreateTime;
        }
        [JsonProperty]
        public string OpenId => this.Id;
        [JsonProperty]
        public virtual string NickName { get; set; }
        [JsonProperty]
        public virtual string Sex { get; set; }
        [JsonProperty]
        public virtual string HeadImgUrl { get; set; }
        [JsonProperty]
        public virtual string PreferedLanguage { get; set; } = "UnSet";
        [JsonProperty]
        public virtual DateTime AccountCreateTime { get; set; } = DateTime.Now;
    }
}
