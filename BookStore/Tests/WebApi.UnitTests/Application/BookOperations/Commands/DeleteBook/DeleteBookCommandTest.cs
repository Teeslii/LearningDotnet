using Xunit;
using TestSetup;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System;
using WebApi.Application.BookOperations.Commands.CreatGenre;
using FluentAssertions;
using System.Linq;
using WebApi.Application.BookOperations.Commands.DeleteGenre;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
      
       public DeleteBookCommandTest(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
           
       }

       [Fact]
       public void WhenBookIdNotFound_InvalidOperationException_ShouldBeReturn()
       {
           DeleteBookCommand command = new DeleteBookCommand(_Context);
           command.BookId = 30;

           FluentActions
                   .Invoking(() => command.Handle())
                   .Should().Throw<InvalidOperationException>().And.Message
                   .Should().Be("The book to be deleted was not found.");
       }

       [Fact]
       public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
       {
            DeleteBookCommand command = new DeleteBookCommand(_Context);
            command.BookId = 1;

            FluentActions
                  .Invoking(() => command.Handle()).Should().NotThrow();
       }
    }
}    