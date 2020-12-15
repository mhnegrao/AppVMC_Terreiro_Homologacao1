using FluentValidation;
using System.Collections.Generic;

namespace AppVMC.Models
{
    public class Cliente : ModelBase
    {

        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string LoginPassword { get; set; }
        public Endereco Endereco { get; set; }
        public string Observacao { get; set; }
    }
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Favor informar um nome de usu�rio");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Favor informar um e-mail v�lido");
            RuleFor(x => x.Celular).NotEmpty().Length(6, 18).WithMessage("Favor informar celular v�lido"); ;
            RuleFor(x => x.LoginPassword).NotEmpty();
        }
    }
}