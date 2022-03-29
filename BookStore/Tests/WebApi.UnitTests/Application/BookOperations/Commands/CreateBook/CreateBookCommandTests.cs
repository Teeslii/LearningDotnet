using Xunit;
using TestSetup;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System;
using WebApi.Application.BookOperations.Commands.CreatBook;
using FluentAssertions;
using System.Linq;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
       private readonly IMapper _mapper;
       public CreateBookCommandTests(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
           _mapper = textFixture.Mapper;
       }

       [Fact]
       public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
       {
           //arrange 
           var book = new Book() {Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", GenreId = 1,  AuthorId = 1,  PageCount=100, PublishDate = new DateTime(1990,06,12)};

           _Context.Books.Add(book);
           _Context.SaveChanges();

           CreateBookCommand command = new CreateBookCommand(_Context, _mapper);
           command.BookModel = new CreateBookModel(){Title = book.Title};

           //act & assert 
           FluentActions
                  .Invoking(() => command.Handle())
                  .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This book is already available.");
       }

       [Fact]
       public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
       {
            CreateBookCommand command = new CreateBookCommand(_Context, _mapper);
            CreateBookModel model = new CreateBookModel(){
                   Title = "Hobbit",  PageCount = 1000, GenreId = 2,  AuthorId = 2, PublishDate = DateTime.Now.Date.AddDays(-10)};
            command.BookModel = model;
            
            // act
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();
            
            // assert
            var book = _Context.Books.SingleOrDefault(book => book.Title == "Hobbit");

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.GenreId.Should().Be(model.GenreId);
            book.PublishDate.Should().Be(model.PublishDate);
             

       }
    }
}