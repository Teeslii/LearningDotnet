using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreatAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2);
              RuleFor(command => command.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}