using System.ComponentModel.DataAnnotations.Schema;
using AiursoftBase.Models;

namespace Developer.Models
{
    public class AppPermission
    {
        public virtual int AppPermissionId { get; set; }

        public virtual string AppId { get; set; }
        [ForeignKey(nameof(AppId))]
        public virtual App App { get; set; }

        public virtual int PermissionId { get; set; }
        [ForeignKey(nameof(PermissionId))]
        public virtual Permission Permission { get; set; }
    }
}