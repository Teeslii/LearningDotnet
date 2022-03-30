using System;
using AutoMapper;
using WebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace Application.GenreOperation.Queries
{ 
    public class GetGenreDetailQueryTests  : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
  
        [Fact]
        public void WhenAlreadyExistExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 58;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Book type not found!");
        }

        [Fact]
        public void WherValidGenreIdIsGiven_Genre_ShouldNotBeReturnError()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 2;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().NotThrow();
        }
    }    
}