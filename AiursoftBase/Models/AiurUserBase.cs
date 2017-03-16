using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AiursoftBase.Models.API.OAuthViewModels;

namespace AiursoftBase.Models
{
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
            this.HeadImgUrl = model.User.Headimgurl;
            this.AccountCreateTime = model.User.AccountCreateTime;
        }
        public virtual string NickName { get; set; }
        public virtual string Sex { get; set; }
        public virtual string HeadImgUrl { get; set; }
        public virtual string PreferedLanguage { get; set; } = "UnSet";
        public virtual DateTime AccountCreateTime { get; set; } = DateTime.Now;
    }
}
