using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Models.API.UserAddressModels
{
    public class ChangeProfileAddressModel
    {
        public string OpenId { get; set; }
        public string NewNickName { get; set; }
        public string AccessToken { get; set; }
    }
}
