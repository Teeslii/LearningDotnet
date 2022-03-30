using System;
using TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
   
       public class GetAuthorDetailQueryValidatorTest : IClassFixture<CommanTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void  WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
            query.AuthorId = id;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotBeReturnErrors(int id)
        {
           
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = id;

            
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

             
            result.Errors.Count.Should().Be(0);
        }
    }    
        
  
}    