using ControleFinanceiro.API.Validacoes.ViewModels;
using ControleFinanceiro.BLL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacoes
{
    public class FuncoesViewModelValidatos: AbstractValidator<FuncoesViewModel>
    {
        public FuncoesViewModelValidatos()
        {
            RuleFor(f => f.Name)
                .NotNull().WithMessage("Preencha a Função")
                .NotEmpty().WithMessage("Preencha a Função")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(30).WithMessage("Use menos caracteres");

            RuleFor(d => d.Descricao)
                .NotNull().WithMessage("Preencha a Descrição")
                .NotEmpty().WithMessage("Preencha a Descrição")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use menos caracteres");
        }
    }
}
