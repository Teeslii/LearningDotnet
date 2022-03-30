using System;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreatGenre;
using Xunit;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class GetBookDetailQueryValidatorTest : IClassFixture<CommanTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void  WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }    
}    