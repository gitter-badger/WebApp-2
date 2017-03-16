using AiursoftBase.Models;
using AiursoftBase.Models.API.OAuthAddressModels;

namespace AiursoftBase.Services
{

    public static class UrlConverter
    {
        private static AiurUrl _GenerateAuthUrl(string Destination, string State = "null")
        {
            var url = new AiurUrl(Values.ApiServerAddress, "oauth", "authorize", new AuthorizeAddressModel
            {
                appid = Values.CurrentAppId,
                redirect_uri = Destination,
                response_type = "code",
                scope = "snsapi_base",
                state = State
            });
            return url;
        }
        private static AiurUrl _GenerateAuthUrl(AiurUrl Destination, string State = "null")
        {
            return _GenerateAuthUrl(Destination.ToString(), State);
        }

        public static string UrlWithAuth(string ServerDomain, string Path)
        {
            return _GenerateAuthUrl(new AiurUrl(ServerDomain, "Auth", "AuthResult", new { }), Path).ToString();
        }
    }
}
