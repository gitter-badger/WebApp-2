using AiursoftBase.Models.OSS;
using Developer.Models.AppsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.BucketViewModels
{
    public class IndexViewModel : AppLayoutModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]
        public IndexViewModel() { }
        public IndexViewModel(DeveloperUser User) : base(User, 2) { }
        public IEnumerable<Bucket> AllBuckets { get; set; }
    }
}
