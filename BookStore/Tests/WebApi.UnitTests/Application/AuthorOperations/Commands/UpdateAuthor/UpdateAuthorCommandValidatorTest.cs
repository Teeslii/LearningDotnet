using System;
using TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace Application.AuthorOptions.Commands.UpdateAuthor
{  
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommanTestFixture>
    {

        [Theory]
        [InlineData("t", "asfasdg")]
        [InlineData("asr", "a")]
        [InlineData("a", "d")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            

            command.Model = new UpdateAuthorModel()
            {
               Name = name,
               Surname = surname
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
       [Theory]
       [InlineData("Ecem", "Güler")]
       [InlineData("Yağız", "Polat")]
       public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(string name, string surname)
       {
           UpdateAuthorCommand command = new UpdateAuthorCommand(null);
           command.Model = new UpdateAuthorModel()
           {
               Name = name,
               Surname = surname
           };

           UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
           var result = validator.Validate(command);

           result.Errors.Count.Should().BeGreaterThan(0);
       }
    }
}   