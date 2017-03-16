using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models
{
    public enum ErrorType : int
    {
        Success = 0,
        WrongKey = -1,
        Pending = -2,
        RequireAttention = -3,
        NotFound = -4,
        UnknownError = -5,
        HasDoneAlready = -6,
        NotEnoughResources = -7,
        Unauthorized = -8,
        InvalidInput = -10,
        Timeout = -11
    }
    public class AiurProtocal
    {
        public virtual ErrorType code { get; set; }
        public virtual string message { get; set; }
    }
}
