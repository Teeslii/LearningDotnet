using System;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreatGenre;
using Xunit;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteGenre;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommanTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
       
        public void WhenInvalidBookIdsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
               DeleteBookCommand command = new DeleteBookCommand(null);
               command.BookId = id;

               DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
               var result = validator.Validate(command);

               result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId= 1;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

       
    }
}        