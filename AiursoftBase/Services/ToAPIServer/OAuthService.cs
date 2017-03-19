using AiursoftBase.Models;
using AiursoftBase.Models.API.OAuthAddressModels;
using AiursoftBase.Models.API.OAuthViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Services.ToAPIServer
{
    public class OAuthService
    {
        public async static Task<CodeToOpenIdViewModel> CodeToOpenIdAsync(int code, string AccessToken)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "OAuth", "CodeToOpenId", new CodeToOpenIdAddressModel
            {
                AccessToken = AccessToken,
                Code = code,
                grant_type = "authorization_code"
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<CodeToOpenIdViewModel>(result);

            if (JResult.code != ErrorType.Success)
                throw new Exception(JResult.message);
            return JResult;
        }

        public async static Task<UserInfoViewModel> OpenIdToUserInfo(string AccessToken, string openid)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "oauth", "UserInfo", new UserInfoAddressModel
            {
                access_token = AccessToken,
                openid = openid,
                lang = "en-US"
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<UserInfoViewModel>(result);
            if (JResult.code != ErrorType.Success)
                throw new Exception(JResult.message);
            return JResult;
        }
    }
}
