using DevFreela.Application.InputModels;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectInputModelValidator : AbstractValidator<UpdateProjectInputModel>
    {
        public UpdateProjectInputModelValidator()
        {
            RuleFor(p => p.Description)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo de Descriçao é de 255 caracteres.");

            RuleFor(p => p.Title)
                .MaximumLength(30)
                .WithMessage("Tamanho máximo de Título é de 30 caracteres");
        }
    }
}
