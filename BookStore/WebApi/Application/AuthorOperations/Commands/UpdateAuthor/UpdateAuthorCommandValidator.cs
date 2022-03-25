using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2).When(x => x.Model.Surname != string.Empty);
        }    
    }
}        