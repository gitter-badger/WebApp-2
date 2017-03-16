using AiursoftBase.Exceptions;
using AiursoftBase.Services;
using AiursoftBase.Services.ToAPIServer;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase
{
    public static class Values
    {
        public static string ProjectName = "Aiursoft";
        public static string Schema = "http";
        public static string Domain { get; private set; } = "obisoft.com.cn";
        public static string @Empty{ get; private set; } = Schema + "://" + Domain;
        public static string DeveloperServerAddress { get; private set; } = Schema + "://developer.aiursoft." + Domain;
        public static string ApiServerAddress { get; private set; } = Schema + "://api.aiursoft." + Domain;
        public static string AccountServerAddress { get; private set; } = Schema + "://account.aiursoft." + Domain;
        public static string OssServerAddress { get; private set; } = Schema + "://oss.aiursoft." + Domain;
        public static string CdnServerAddress { get; private set; } = Schema + "://cdn.aiursoft." + Domain;
        public static string WikiServerAddress { get; private set; } = Schema + "://wiki.aiursoft." + Domain;
        public static string MqServerAddress { get; private set; } = Schema + "://mp.aiursoft." + Domain;
        public static string HrServerAddress { get; private set; } = Schema + "://hr.aiursoft." + Domain;
        public static string WWWServerAddress { get; private set; } = Schema + "://www.aiursoft." + Domain;
        public static string ForumServerAddress { get; private set; } = Schema + "://forum.aiursoft." + Domain;

        public static string CurrentAppId { get; private set; } = string.Empty;
        public static string CurrentAppSecret { get; private set; } = string.Empty;
        public static int AppsIconBucketId { get; set; } = 67;

        public async static Task<string> AccessToken()
        {
            if (DateTime.Now > _accessTokenDeadTime)
            {
                var ServerResult = await APIService.AccessTokenAsync(CurrentAppId, CurrentAppSecret);
                _latestAccessToken = ServerResult.AccessToken;
                _accessTokenDeadTime = ServerResult.DeadTime;
            }
            return _latestAccessToken;
        }

        private static string _latestAccessToken { get; set; } = string.Empty;
        private static DateTime _accessTokenDeadTime { get; set; } = DateTime.MinValue;

        public static IApplicationBuilder UseAiursoftAuthentication(this IApplicationBuilder app, string appId, string appSecret, string ServerAddress = "")
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new NotAValidAiurArgumentException(nameof(appId));
            }
            if (string.IsNullOrWhiteSpace(appSecret))
            {
                throw new NotAValidAiurArgumentException(nameof(appSecret));
            }
            if (!string.IsNullOrWhiteSpace(ServerAddress))
            {
                ApiServerAddress = ServerAddress;
            }
            CurrentAppId = appId;
            CurrentAppSecret = appSecret;
            return app;
        }
    }
}
