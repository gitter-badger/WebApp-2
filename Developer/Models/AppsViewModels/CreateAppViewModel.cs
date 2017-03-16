using AiursoftBase.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models.AppsViewModels
{
    public class CreateAppViewModel : AppLayoutModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]
        public CreateAppViewModel() { }
        public CreateAppViewModel(DeveloperUser User) : base(User, 1) { }

        public bool ModelStateValid { get; set; } = true;

        [Display(Name = "App Name")]
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 1)]
        public virtual string AppName { get; set; }

        [Display(Name = "App Description")]
        public virtual string AppDescription { get; set; }

        [Required]
        [Display(Name = "App Category")]
        public virtual Category AppCategory { get; set; }

        [Required]
        [Display(Name = "App Platform")]
        public virtual Platform AppPlatform { get; set; }
    }
}
