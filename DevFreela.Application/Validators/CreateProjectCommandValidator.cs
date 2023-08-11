using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator() 
        {
            RuleFor(p => p.Description)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo da Descrição é de 255 caracteres.");

            RuleFor(p => p.Title)
                .MaximumLength(50)
                .WithMessage("Tamanho máximo do Título é de 50 caracteres.");
        }
    }
}
