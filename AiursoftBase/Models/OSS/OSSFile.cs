using AiursoftBase.Models.OSS.ApiViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models.OSS
{
    public class OSSFile
    {
        [Key]
        public int FileKey { get; set; }
        public string RealFileName { get; set; }
        public string FileExtension { get; set; }

        [NotMapped]
        public long JFileSize { get; set; }

        public int BucketId { get; set; }
        [ForeignKey(nameof(BucketId))]
        [JsonIgnore]
        public Bucket BelongingBucket { get; set; }

        private string _internetPath { get; set; } = null;
        [NotMapped]
        [JsonProperty(PropertyName = nameof(GetInternetPath))]
        public virtual string GetInternetPath
        {
            get
            {
                if (this._internetPath == null)
                {
                    _internetPath = new AiurUrl(Values.OssServerAddress, BelongingBucket.BucketName, RealFileName, new { }).ToString();
                }
                return _internetPath;
            }
            set { this._internetPath = value; }
        }
    }
}
