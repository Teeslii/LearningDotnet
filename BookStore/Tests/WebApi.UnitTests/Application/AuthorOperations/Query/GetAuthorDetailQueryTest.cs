using System;
using AutoMapper;
using WebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace Application.AuthorOperations.Queries
{ 
    public class GetAuthorDetailQueryTest : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTest(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
  
        [Fact]
        public void WhenAlreadyExistExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 12;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("The author was not found.");
        }

        [Fact]
        public void WherValidAuthorIdIsGiven_Author_ShouldNotBeReturnError()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 2;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().NotThrow();
        }
    }   
} 