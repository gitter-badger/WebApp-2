using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.Models.AccountViewModels
{
    public class AvatarViewModel : AccountViewModel
    {
        [Obsolete(error: true, message: "This method is only for framework!")]
        public AvatarViewModel() { }
        public AvatarViewModel(AccountUser user) : base(user, 1, "Avatar") { }
        public void Recover(AccountUser user)
        {
            base.Recover(user, 0, "Profile");
        }
    }
}
