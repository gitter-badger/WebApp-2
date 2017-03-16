using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AiursoftBase.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Models
{
    public class AccountUser : AiurUserBase
    {
        [InverseProperty(nameof(UserJoin.ThatUser))]
        public virtual IEnumerable<UserJoin> JoinedOrganizations { get; set; }

        public virtual string Bio { get; set; }

        public virtual string URL { get; set; }

        public virtual string Company { get; set; }
        public virtual string Location { get; set; }
    }

    public class UserJoin
    {
        public virtual int UserJoinId { get; set; }
        public virtual DateTime JoinTime { get; set; }

        public virtual string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AccountUser ThatUser { get; set; }

        public virtual int OrganziationId { get; set; }
        [ForeignKey(nameof(OrganziationId))]
        public virtual Organization Organization { get; set; }

    }
    public class Organization
    {
        public virtual int OrganizationId { get; set; }
        public virtual string OrganizatioName { get; set; }
        public virtual DateTime CreateTime { get; set; }

        [InverseProperty(nameof(UserJoin.Organization))]
        public virtual IEnumerable<UserJoin> JoinedUsers { get; set; }
    }
}
