using FluentValidation;

namespace  WebApi.Application.GenreOperations.Commands.CreateGenra
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
          public CreateGenreCommandValidator()
          {
              RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
          }
    }
}    