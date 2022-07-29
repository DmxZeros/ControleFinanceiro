using ControleFinanceiro.API.Validacoes.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacoes
{
    public class RegistroViewModelValidator : AbstractValidator<RegistroViewModel>
    {
        public RegistroViewModelValidator()
        {
            RuleFor(x => x.NomeUsuario)
                .NotNull().WithMessage("Preencha o nome do usuário")
                .NotEmpty().WithMessage("Preencha o nome do usuário")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use menos caracteres");

            RuleFor(x => x.CPF)
                .NotNull().WithMessage("Preencha o CPF")
                .NotEmpty().WithMessage("Preencha o CPF")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(20).WithMessage("Use menos caracteres");

            RuleFor(x => x.Profissao)
                .NotNull().WithMessage("Preencha a profissão")
                .NotEmpty().WithMessage("Preencha a profissão")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(30).WithMessage("Use menos caracteres");

            RuleFor(x => x.Foto)
                .NotNull().WithMessage("Escolha a foto")
                .NotEmpty().WithMessage("Escolha a foto");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Preencha o email")
                .NotEmpty().WithMessage("Preencha o email")
                .MinimumLength(10).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use menos caracteres")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(x => x.Senha)
                .NotNull().WithMessage("Preencha a senha")
                .NotEmpty().WithMessage("Preencha a senha")
                .MinimumLength(6).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use menos caracteres");

        }
    }
}
