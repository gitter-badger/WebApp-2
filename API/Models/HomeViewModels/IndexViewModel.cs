using AiursoftBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.HomeViewModels
{
    public class IndexViewModel : AiurProtocal
    {
        public DateTime ServerTime { get; internal set; }
        public bool Signedin { get; internal set; }
        public string UserId { get; internal set; }
        public string UserImage { get; internal set; }
        public string UserName { get; internal set; }
    }
}
