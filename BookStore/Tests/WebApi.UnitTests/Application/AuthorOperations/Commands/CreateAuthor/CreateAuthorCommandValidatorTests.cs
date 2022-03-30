using System;
using TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreatAuthor;

namespace Application.AuthorOptions.Commands.CreateAuthor
{
    public class  CreateAuthorCommandValidatorTests: IClassFixture<CommanTestFixture>
    {
        [Theory]
        [InlineData("n", "a")]
        [InlineData(" ", " ")]
        [InlineData("a", "")]
        [InlineData(" ", "a")]
       public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
       {
           CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Now.AddYears(-50)
            };
        
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);   

           
            result.Errors.Count.Should().BeGreaterThan(0);
       }
     
     

       [Fact]
       public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors()
       {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Ezgi", 
                Surname = "Yol", 
                DateOfBirth = DateTime.Now.Date
                };
                 
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);   

          
            result.Errors.Count.Should().BeGreaterThan(0);
       }
 
       [Fact]
       public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
       {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                   Name = "Görkem",
                   Surname = "Güler",
                   DateOfBirth = DateTime.Now.Date.AddDays(-30)
            };
                 
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);   
   
            result.Errors.Count.Should().Be(0);
       }
        
    }
}        
