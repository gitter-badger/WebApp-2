using AiursoftBase;
using AiursoftBase.Exceptions;
using AiursoftBase.Models;
using AiursoftBase.Models.OSS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AiursoftBase.Services.ToOSSServer;
using AiursoftBase.Models.API;

namespace Developer.Models.AppsViewModels
{
    public class ViewAppViewModel : CreateAppViewModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]
        public ViewAppViewModel() { }
        public static async Task<ViewAppViewModel> SelfCreateAsync(DeveloperUser User, App ThisApp)
        {
            var model = new ViewAppViewModel(User, ThisApp);
            await model.Recover(User, ThisApp);
            return model;
        }
        public async Task Recover(DeveloperUser User, App ThisApp)
        {
            base.Recover(User, 1);
            var token = AppsContainer.AccessToken(ThisApp.AppId, ThisApp.AppSecret);

            var buckets = await ApiService.ViewMyBucketsAsync(await token());
            Buckets = buckets.Buckets;

            var grants = await AiursoftBase.Services.ToAPIServer.APIService.AllUserGrantedAsync(await token());
            Grants = grants.Grants;
        }
        private ViewAppViewModel(DeveloperUser User, App ThisApp) : base(User)
        {
            if (ThisApp.CreaterId != User.Id)
            {
                throw new AiurCrossAuthorityException("The app is not the user's app!");
            }
            this.AppName = ThisApp.AppName;
            this.AppDescription = ThisApp.AppDescription;
            this.AppCategory = ThisApp.AppCategory;
            this.AppPlatform = ThisApp.AppPlatform;
            this.AppId = ThisApp.AppId;
            this.AppSecret = ThisApp.AppSecret;
            this.EnableOAuth = ThisApp.EnableOAuth;
            this.ForceInputPassword = ThisApp.ForceInputPassword;
            this.ForceConfirmation = ThisApp.ForceConfirmation;
            this.DebugMode = ThisApp.DebugMode;
            this.PrivacyStatementUrl = ThisApp.PrivacyStatementUrl;
            this.LicenseUrl = ThisApp.LicenseUrl;
            this.AppIconAddress = ThisApp.AppIconAddress;
            this.AppDomain = ThisApp.AppDomain;
        }

        public virtual bool JustHaveUpdated { get; set; } = false;
        public virtual string AppId { get; set; }
        public virtual string AppSecret { get; set; }
        public virtual string AppIconAddress { get; set; }
        [Url]
        [Display(Name = "Privacy Statement Url")]
        public virtual string PrivacyStatementUrl { get; set; }
        [Url]
        [Display(Name = "License Url")]
        public virtual string LicenseUrl { get; set; }

        [Display(Name = "Enable OAuth")]
        public virtual bool EnableOAuth { get; set; }
        [Display(Name = "Force Input Password")]
        public virtual bool ForceInputPassword { get; set; }
        [Display(Name = "Force Confirmation")]
        public virtual bool ForceConfirmation { get; set; }
        [Display(Name = "Debug Mode")]
        public virtual bool DebugMode { get; set; }
        [Display(Name = "App Domain")]
        public virtual string AppDomain { get; set; }

        public IEnumerable<Bucket> Buckets { get; set; } //= new List<Bucket>();
        public IEnumerable<Grant> Grants { get; set; }
        public IEnumerable<ViewAblePermission> ViewAblePermission { get; set; }
    }

    public class ViewAblePermission
    {
        public virtual int PermissionId { get; set; }
        public virtual string PermissionName { get; set; }
        public virtual bool Permitted { get; set; }
    }
}
