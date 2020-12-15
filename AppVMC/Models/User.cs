using FluentValidation;
using System;

namespace AppVMC.Models
{
    //[DisplayForClass(Name = "Usuario")]
    public class User : ModelBase
    {
        /// <summary>
        /// Constante usada para validação de nomes em api
        /// </summary>
        //public const string displayName = "usuario";
        public User()
        {

            DataInclusao = DateTime.Now;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        public int IdCategoria { get; set; }

        public bool IsValid()
        {
            UserValidator validator = new UserValidator();
            var results = validator.Validate(this);
            if (!results.IsValid)
            {
                //foreach (var failure in results.Errors) {
                // Console.WriteLine ("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                //}
                return false;
            }
            return true;
        }
    }
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Favor informar um nome de usuário");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Favor informar um e-mail válido");
            RuleFor(x => x.Password).NotEmpty().Length(6, 15);
            RuleFor(x => x.PasswordConfirm).NotEmpty();
        }
    }
}