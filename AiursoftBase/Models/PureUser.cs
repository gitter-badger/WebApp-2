using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Models
{
    public class PureUser
    {
        [Obsolete(message: "for json deserilize", error: true)]
        public PureUser() { }
        public PureUser(AiurUserBase ServerUser)
        {
            this.OpenId = ServerUser.Id;
            this.NickName = ServerUser.NickName;
            this.Sex = ServerUser.Sex;
            this.Headimgurl = ServerUser.HeadImgUrl;
            this.PreferedLanguage = ServerUser.PreferedLanguage;
            this.AccountCreateTime = ServerUser.AccountCreateTime;
        }
        public virtual string OpenId { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Sex { get; set; }
        public virtual string Headimgurl { get; set; }
        public virtual string PreferedLanguage { get; set; } = "UnSet";
        public virtual DateTime AccountCreateTime { get; set; } = DateTime.Now;
    }
}
