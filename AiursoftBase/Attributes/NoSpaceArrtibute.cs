using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AiursoftBase.Attributes
{
    public class NoSpaceAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string val = value as string;
            if (val != null)
            {
                return !val.Contains(" ");
            }
            return true;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValid(value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"The {validationContext.DisplayName} can not contains space!");
            }
        }
    }
}
