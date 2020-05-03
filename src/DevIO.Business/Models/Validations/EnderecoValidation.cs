using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            RuleFor(e => e.Logradouro)
                .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido")
                .Length(2, 200)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght} caracteres");

            RuleFor(e => e.Numero)
               .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido")
               .Length(1, 30)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght} caracteres");
            
            RuleFor(e => e.Bairro)
               .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido")
               .Length(2, 200)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght} caracteres");

            RuleFor(e => e.Cep)
               .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido")
               .Length(8, 8)
               .WithMessage("O campo {PropertyName} precisa ter {MaxLenght} caracteres");
            
            RuleFor(e => e.Cidade)
               .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido")
               .Length(2, 50)
               .WithMessage("O campo {PropertyName} precisa ter {MaxLenght} caracteres");

            RuleFor(e => e.Estado)
               .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido")
               .Length(2, 50)
               .WithMessage("O campo {PropertyName} precisa ter {MaxLenght} caracteres");
        }
    }
}
