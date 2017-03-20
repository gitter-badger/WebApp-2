using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AiursoftBase.Models.API
{
    public interface IAppGrant
    {
        string AppID { get; set; }
        DateTime GrantTime { get; set; }// = DateTime.Now;
        string APIUserId { get; set; }
        AiurUserBase UserInfo { get; }
    }
}
