 
using System;
using TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace Application.AuthorOptions.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest : IClassFixture<CommanTestFixture>
    {
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenInvalidAuthorIdsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
               DeleteAuthorCommand command = new DeleteAuthorCommand(null);
               command.AuthorId = id;

               DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
               var result = validator.Validate(command);

               result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidBookIdsAreGiven_Validator_ShouldNotBeReturnErrors(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

       
    }
}        

 