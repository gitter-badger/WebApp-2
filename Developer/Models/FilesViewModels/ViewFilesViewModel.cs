using AiursoftBase;
using AiursoftBase.Models.OSS;
using AiursoftBase.Models.OSS.ApiViewModels;
using Developer.Models.AppsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.FilesViewModels
{
    public class ViewFilesViewModel : AppLayoutModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]
        public ViewFilesViewModel() { }
        public ViewFilesViewModel(DeveloperUser User) : base(User, 3) { }
        public virtual int BucketId { get; set; }
        public IEnumerable<OSSFile> AllFiles { get; set; }
        public virtual string AppId { get; set; }
    }
}
