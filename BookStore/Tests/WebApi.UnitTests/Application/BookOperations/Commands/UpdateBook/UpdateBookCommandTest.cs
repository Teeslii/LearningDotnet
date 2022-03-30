using Xunit;
using TestSetup;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System;
using WebApi.Application.BookOperations.Commands.CreatGenre;
using FluentAssertions;
using System.Linq;
using WebApi.Application.BookOperations.Commands.UpdateBook;

namespace Application.BookOperations.Commands.CreateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
       private readonly IMapper _mapper;
       public UpdateBookCommandTest(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
           _mapper = textFixture.Mapper;
       }

       [Fact]
       public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
       {
           UpdateBookCommand command = new UpdateBookCommand(_Context);
           command.BookId = 45;

           FluentActions
                 .Invoking(() => command.Handle())
                 .Should()
                 .Throw<InvalidOperationException>()
                 .And.Message.Should().Be("The book to be updated could not be found.");
       }

       [Fact]
       public void WhenValidBookIdAreGiven_Book_ShouldBeUpdated()
       {
           UpdateBookCommand command = new UpdateBookCommand(_Context);
           command.BookId = 3;

           UpdateBookModel model = new UpdateBookModel()
           {
                 Title = "Lord Of The Rings Update",
                 GenreId = 1
           };

           command.Model = model;

           FluentActions
                .Invoking(() => command.Handle()).Invoke();

           var book = _Context.Books.SingleOrDefault( book => book.Title == model.Title);

           book.Should().NotBeNull();
           book.GenreId.Should().Be(model.GenreId);     
       }

    }
}       