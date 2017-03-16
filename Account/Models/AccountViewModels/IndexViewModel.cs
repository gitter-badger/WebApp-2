using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Account.Models.AccountViewModels
{
    public class IndexViewModel : AccountViewModel
    {
        [Obsolete(error: true, message: "This method is only for framework!")]
        public IndexViewModel() { }
        public IndexViewModel(AccountUser user) : base(user, 0, "Profile")
        {
            Recover(user);
        }
        public void Recover(AccountUser user)
        {
            base.Recover(user, 0, "Profile");
            this.NickName = user.NickName;
            this.Bio = user.Bio;
            this.URL = user.URL;
            this.Company = user.Company;
            this.Location = user.Location;
        }
        [Display(Name = "Nick name")]
        [Required]
        public virtual string NickName { get; set; }
        public virtual string Bio { get; set; }

        public virtual string URL { get; set; }

        public virtual string Company { get; set; }
        public virtual string Location { get; set; }
    }
}
