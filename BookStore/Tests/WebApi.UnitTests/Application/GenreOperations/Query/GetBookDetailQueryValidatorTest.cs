using System;
using TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace Application.GenreOperations.Queries
{
   
       public class  GetGenreDetailQueryValidatorTest: IClassFixture<CommanTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void  WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }    
  
} 