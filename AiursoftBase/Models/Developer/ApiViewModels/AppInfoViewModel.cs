using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models.Developer.ApiViewModels
{
    public class AppInfoViewModel : AiurProtocal
    {
        public virtual string AppId { get; set; }
        public virtual string AppName { get; set; }
        public virtual string AppDescription { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy - MM - dd}")]
        public virtual DateTime AppCreateTime { get; set; }
        public virtual string CreaterId { get; set; }

        public virtual Category AppCategory { get; set; }
        public virtual Platform AppPlatform { get; set; }

        public virtual bool EnableOAuth { get; set; }
        public virtual bool ForceInputPassword { get; set; }
        public virtual bool ForceConfirmation { get; set; }
        public virtual bool DebugMode { get; set; }
        public virtual string AppDomain { get; set; }
        public virtual string PrivacyStatementUrl { get; set; }
        public virtual string LicenseUrl { get; set; }
        public virtual string AppImageUrl { get; set; }
    }
}
