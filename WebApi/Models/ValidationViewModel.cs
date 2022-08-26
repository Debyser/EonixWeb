using System.Collections.Generic;
using System.Linq;
using EonixWebApi.ApplicationCore.Validations;

namespace EonixWebApi.Web.Models
{
    internal sealed class ValidationViewModel
    {
        public string GlobalLevel
        {
            get
            {
                if (Errors.Any())
                    return ValidationLevel.Error.ToString();
                if (Warnings.Any())
                    return ValidationLevel.Warning.ToString();
                return Information.Any() ? ValidationLevel.Information.ToString() : ValidationLevel.None.ToString();
            }
        }

        public IList<ValidationMessageViewModel> Errors { get; } = new List<ValidationMessageViewModel>();
        public IList<ValidationMessageViewModel> Warnings { get; } = new List<ValidationMessageViewModel>();
        public IList<ValidationMessageViewModel> Information { get; } = new List<ValidationMessageViewModel>();
    }
}
