 using System;
using TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest: IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext _context;
       public DeleteGenreCommandValidatorTest(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
       
        public void WhenInvalidGenreIdsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
               DeleteGenreCommand command = new DeleteGenreCommand(_context);
               command.GenreId  = id;

               DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
               var result = validator.Validate(command);  
 
               result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidGenreIdsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;

            DeleteGenreCommandValidator validator = new  DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

       
    }
}        

 