using ControleFinanceiro.BLL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacoes
{
    public class DespesaValidator: AbstractValidator<Despesa>
    {
        public DespesaValidator()
        {
            RuleFor(d => d.CartaoId)
                .NotEmpty().WithMessage("Escolha o cartão")
                .NotNull().WithMessage("Escolha o cartão");

            RuleFor(d => d.Descricao)
                .NotEmpty().WithMessage("Preencha a descrição")
                .NotNull().WithMessage("Preencha a descrição")
                .MinimumLength(1).WithMessage("Preencha a descrição")
                .MaximumLength(50).WithMessage("Máximo de 50 caracteres");

            RuleFor(d => d.Valor)
                .NotEmpty().WithMessage("Preencha o valor")
                .NotNull().WithMessage("Preencha o valor")
                .InclusiveBetween(0, double.MaxValue).WithMessage("Valor inválido");

            RuleFor(d => d.MesId)
                .NotEmpty().WithMessage("Escolha um mês")
                .NotNull().WithMessage("Escolha um mês");

            RuleFor(d => d.Ano)
                .NotEmpty().WithMessage("Preencha o ano")
                .NotNull().WithMessage("Preencha o ano"); ;

        }
    }
}
