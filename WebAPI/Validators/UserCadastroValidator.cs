using FluentValidation;
using WebAplicationPessoa.WebAPI.DTOs;

namespace WebAplicationPessoa.WebAPI.Validators;

public class UserCadastroValidator : AbstractValidator<UserCadastroDto>
{
    public UserCadastroValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("O e-mail deve ser informado.")
            .EmailAddress()
            .WithMessage("O campo deve ser do tipo e-mail.");
        
        RuleFor(user => user.Senha)
            .NotEmpty()
            .WithMessage("A senha deve ser informada.")
            .MinimumLength(6)
            .WithMessage("A senha deve conter no mínimo 6 caracteres.");

        RuleFor(user => user.Nome)
            .NotEmpty()
            .WithMessage("O nome deve ser informado.")
            .MinimumLength(10)
            .WithMessage("O campo Nome deve conter no mínimo 10 caracteres.");
    }
}