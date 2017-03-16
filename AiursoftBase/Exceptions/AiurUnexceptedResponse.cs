using AiursoftBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Exceptions
{
    public class AiurUnexceptedResponse : Exception
    {
        public AiurProtocal Response { get; set; }
        public AiurUnexceptedResponse(AiurProtocal response)
        {
            this.Response = response;
        }
    }
}
