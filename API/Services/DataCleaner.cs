using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;

namespace API.Services
{
    public class DataCleaner
    {
        public APIDbContext _dbContext;
        public int TimeDelay;
        public DataCleaner(APIDbContext _dbContext, int TimeDelay = 60000)
        {
            this._dbContext = _dbContext;
            this.TimeDelay = TimeDelay;
        }
        public void StartCleanerService()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await TimeoutCleaner.AllClean(_dbContext);
                    await Task.Delay(TimeDelay);
                }
            });
        }
    }
}
