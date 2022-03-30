using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreatGenre
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.BookModel.GenreId).GreaterThan(0);
            RuleFor(command => command.BookModel.PageCount).GreaterThan(0);
            RuleFor(command => command.BookModel.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.BookModel.Title).NotEmpty().MinimumLength(4); 
        }

    }
}