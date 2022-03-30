
 using Xunit;
using TestSetup;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System;
using FluentAssertions;
using System.Linq;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace Application.AuthorOptions.Commands.DeleteAuthor
{
    public class DeleteBookCommandTest : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
      
       public DeleteBookCommandTest(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
           
       }

       [Fact]
       public void WhenAuthorIdNotFound_InvalidOperationException_ShouldBeReturnErrors()
       {
           DeleteAuthorCommand command = new DeleteAuthorCommand(_Context);
           command.AuthorId = 15;

           FluentActions
                   .Invoking(() => command.Handle())
                   .Should().Throw<InvalidOperationException>().And.Message
                   .Should().Be("Author not found!");
       }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenAuthorHasABook_InvalidOperationException_ShouldBeReturnErrors(int Id)
        {
           
            DeleteAuthorCommand command = new DeleteAuthorCommand(_Context);
            command.AuthorId = Id;

            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The author cannot be deleted without deleting the book.");
        }

    }
}    
