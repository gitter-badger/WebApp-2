using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using API.Data;
using AiursoftBase.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AiursoftBase.Models.API;

namespace API.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class APIUser : AiurUserBase
    {
        [InverseProperty(nameof(OAuthPack.User))]
        public virtual List<OAuthPack> Packs { get; set; }
        [InverseProperty(nameof(LocalAppGrant.User))]
        public virtual List<LocalAppGrant> GrantedApps { get; set; }

        public async virtual Task GrantTargetApp(APIDbContext DbContext, string AppId)
        {
            if (await HasAuthorizedApp(DbContext, AppId))
            {
                return;
            }
            var AppGrant = new LocalAppGrant
            {
                AppID = AppId,
                APIUserId = this.Id
            };
            DbContext.LocalAppGrant.Add(AppGrant);
            await DbContext.SaveChangesAsync();
        }
        public async virtual Task<OAuthPack> GeneratePack(APIDbContext DbContext, string AppId)
        {
            var pack = new OAuthPack
            {
                //AccessTokens = new List<AccessToken>(),
                Code = (Id + DateTime.Now.ToString()).GetHashCode(),
                UserId = this.Id,
                ApplyAppId = AppId
            };
            DbContext.OAuthPack.Add(pack);
            await DbContext.SaveChangesAsync();
            return pack;
        }
        public async Task<bool> HasAuthorizedApp(APIDbContext DbContext, string appId)
        {
            var appGrant = await DbContext.LocalAppGrant.SingleOrDefaultAsync(t => t.AppID == appId && t.APIUserId == this.Id);
            return appGrant != null;
        }
    }

}
