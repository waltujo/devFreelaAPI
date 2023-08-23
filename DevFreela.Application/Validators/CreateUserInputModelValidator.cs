using DevFreela.Application.InputModels;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators
{
    public class CreateUserInputModelValidator : AbstractValidator<CreateUserInputModel>
    {
        public CreateUserInputModelValidator()
        {
            RuleFor(p => p.Email).EmailAddress().WithMessage("E-mail não válido!");
            RuleFor(p => p.Password).Must(ValidPassword).WithMessage("Senha deve conter pelo meno 8 caracteres, um número, uma letra maiúscula e um caractere especial");
            RuleFor(p => p.FullName).NotEmpty().NotNull().WithMessage("Nome deve ser preenchido");
        }

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            return regex.IsMatch(password);
        }
    }
}
