using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models.API.OAuthAddressModels
{
    public class AuthorizeAddressModel
    {
        [Required]
        public string appid { get; set; }
        [Required]
        [Url]
        public string redirect_uri { get; set; }
        public string state { get; set; }
        public string scope { get; set; }
        public string response_type { get; set; }
        public bool ForceSignInLocally { get; set; } = false;
        public string GetRegexRedirectUrl()
        {
            var url = new Uri(redirect_uri);
            string result = $@"{url.Scheme}://{url.Host}:{url.Port}{url.AbsolutePath}";
            return result;
        }
        public IOAuthInfo Convert(string email)
        {
            return new OAuthInfo
            {
                AppId = appid,
                Email = email,
                State = state,
                ToRedirect = redirect_uri,
                Scope = scope,
                ResponseType = response_type
            };
        }
    }
}
