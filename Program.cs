using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using WebAplicationPessoa.WebAPI.Data;
using WebAplicationPessoa.WebAPI.Middlewares;
using WebAplicationPessoa.WebAPI.Model;
using WebAplicationPessoa.WebAPI.Repository;
using WebAplicationPessoa.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
// No Program.cs, certifique-se de que esta linha existe:
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();