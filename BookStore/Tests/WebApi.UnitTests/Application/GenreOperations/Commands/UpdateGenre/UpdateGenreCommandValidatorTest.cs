using System;
using TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Application.GenreOperations.Commands.UpdateGenre
{  
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommanTestFixture>
    {

        [Theory]
       
        [InlineData(" ")]
        [InlineData("te")]
        [InlineData("tes")]
        [InlineData("t")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors( string name)
        {
             UpdateGenreCommand command = new UpdateGenreCommand(null);
             

            command.Model= new UpdateGenreModel()
            {
                  Name = name
            };

            UpdateGenreCommandValidator validator = new   UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
       [Fact]
       public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
       {
          UpdateGenreCommand command = new UpdateGenreCommand(null);
           command.GenreId = 1;
           command.Model = new UpdateGenreModel()
           {
              Name = "GenreNameUpdateTest"
           };

           UpdateGenreCommandValidator validator = new  UpdateGenreCommandValidator();
            
           var result = validator.Validate(command);

           result.Errors.Count.Should().Be(0);
       }
    }
}     