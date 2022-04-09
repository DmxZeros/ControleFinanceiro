using ControleFinanceiro.BLL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacoes
{
    public class CategoriaValidator: AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Nome)
                .NotNull().WithMessage("Preencha o nome")
                .NotEmpty().WithMessage("Preencha o nome")
                .MinimumLength(3).WithMessage("Mínimo 3 caracteres")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres");

            RuleFor(c => c.Icone)
                .NotNull().WithMessage("Preencha o nome")
                .NotEmpty().WithMessage("Preencha o nome")
                .MinimumLength(1).WithMessage("Mínimo 1 caracteres")
                .MaximumLength(15).WithMessage("Máximo 15 caracteres");

            RuleFor(c => c.TipoId)
                .NotNull().WithMessage("Preencha o nome")
                .NotEmpty().WithMessage("Preencha o nome");
        }
    }
}
