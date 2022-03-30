using System;
using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Queries 
{
    public class GetBookDetailQueryTest : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTest(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
  
        [Fact]
        public void WhenAlreadyExistExistBookIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 43;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("The book was not found.");
        }

        [Fact]
        public void WherValidBookIdIsGiven_Book_ShouldNotBeReturnError()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 2;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().NotThrow();
        }
    }    
}