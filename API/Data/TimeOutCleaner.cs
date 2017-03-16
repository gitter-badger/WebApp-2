using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public static class TimeoutCleaner
    {
        public static async Task AllClean(APIDbContext _dbContext)
        {
            await ClearTimeOutAccessToken(_dbContext);
            await ClearTimeOutOAuthPack(_dbContext);
        }
        public static Task ClearTimeOutAccessToken(APIDbContext _dbContext)
        {
            var _toDel = _dbContext.AccessToken
                .ToList()
                .Where(t => !t.IsAlive);

            _dbContext.AccessToken.RemoveRange(_toDel);
            return _dbContext.SaveChangesAsync();
        }

        public static Task ClearTimeOutOAuthPack(APIDbContext _dbContext)
        {
            var _UsedAndNoToken = _dbContext.OAuthPack
                .Where(t => t.IsUsed == true);

            var _Timeout = _dbContext.OAuthPack
                .ToList()
                .Where(t => !t.IsAlive);

            _dbContext.OAuthPack.RemoveRange(_UsedAndNoToken);
            _dbContext.OAuthPack.RemoveRange(_Timeout);
            return _dbContext.SaveChangesAsync();
        }
    }
}
