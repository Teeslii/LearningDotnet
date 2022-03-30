using System;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreatGenre;
using Xunit;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommanTestFixture>
    {

        [Theory]
        [InlineData(0,"",1)]
        [InlineData(0, "Lor", 1)]
        [InlineData(0, "Lord", 0)]
        [InlineData(1, "", 1)]
        [InlineData(1, "Lor", 1)]
        [InlineData(1, "Lord", 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string title, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = id;

            command.Model= new UpdateBookModel()
            {
                 Title = title,
                 GenreId = genreId
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
       public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
       {
           UpdateBookCommand command = new UpdateBookCommand(null);
           command.Model = new UpdateBookModel()
           {
               Title = "Lord Of The Rings Update validator",
               GenreId = 2
           };

           UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
           var result = validator.Validate(command);

           result.Errors.Count.Should().BeGreaterThan(0);
       }
    }
}    