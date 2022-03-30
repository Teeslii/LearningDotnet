using System;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreatGenre;
using Xunit;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenra;

namespace Application.BookOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommanTestFixture>
    {
       [Theory]
       [InlineData("")]
       [InlineData(null)]
       [InlineData("tes")]
       public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
       {
            // arrange
            CreateGenreCommand  command= new CreateGenreCommand(null);
            command.Model = new CreateGenreModel()
            { 
                   Name = name
            };  

            // act 
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);   

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
       }
      
       [Fact]
       public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
       {
           CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel()
            {
                   Name ="addedGenreNames"
            };
                 
           CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);  
   
            result.Errors.Count.Should().Be(0);
       }
        
    }
}        


 