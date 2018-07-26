using MTL.WebAPI.ViewModels.Validations;
using FluentValidation.Attributes;

namespace MTL.WebAPI.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
