using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Npgsql.Replication.PgOutput.Messages;
using WebAplicationPessoa.WebAPI.Data;
using WebAplicationPessoa.WebAPI.DTOs;

namespace WebAplicationPessoa.WebAPI.Validators;

public class PessoaCreateValidator :  AbstractValidator<PessoaCreateDto>
{
    public PessoaCreateValidator(AppDbContext context)
    {
        RuleFor(e => e.Cpf)
            .NotEmpty()
            .WithMessage("O CPF deve ser informado")
            .MustAsync(async (cpf, cancellation) => !await context.Pessoas.AnyAsync(p => p.Cpf == cpf))
            .WithMessage("Este CPF ja está cadastrado!")
            .MaximumLength(11)
            .WithMessage("O campo CPF deve ter no máximo 11 caracteres.");

        RuleFor(e => e.Nome)
            .NotEmpty()
            .WithMessage("O nome deve ser informado")
            .MustAsync(async (nome, cancellation) => !await context.Pessoas.AnyAsync(p => p.Nome == nome))
            .WithMessage("Este nome já esta cadastrado!")
            .MaximumLength(50)
            .WithMessage("O campo Nome deve ter no máximo 50 caracteres.");

        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("O e-mail deve ser informado")
            .EmailAddress()
            .WithMessage("O e-mail informado não é válido")
            .Must(email => email.ToLower().EndsWith("gmail.com"))
            .WithMessage("Apenas contas @gmail.com são permitidas neste cadastro.")
            .MustAsync( async (email , cancellation) => !await context.Pessoas.AnyAsync(p => p.Email == email))
            .WithMessage("Este e-mail ja esta cadastrado!")
            .MaximumLength(50)
            .WithMessage("O campo Gmail deve ter no máximo 50 caracteres.");

        RuleFor(e => e.Telefone)
            .NotEmpty()
            .WithMessage("O telefone deve ser informado")
            .MustAsync( async (telefone, cancellation) => !await context.Pessoas.AnyAsync(e => e.Telefone == telefone))
            .WithMessage("Este telefone ja esta cadastrado!")
            .MaximumLength(16)
            .WithMessage("O campo telefone deve ter no máximo 16 caracteres.");
        

    }
}