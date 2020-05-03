using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DevIO.Business.Models.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
           
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido")
                .Length(2, 200)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght} caracteres");

            RuleFor(c => c.Descricao)
               .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido")
               .Length(2, 1000)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght} caracteres");

            RuleFor(c => c.Valor)
              .GreaterThan(0).WithMessage("o {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}
