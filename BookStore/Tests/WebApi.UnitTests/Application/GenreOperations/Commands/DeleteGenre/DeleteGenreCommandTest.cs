using Xunit;
using TestSetup;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using System.Linq;
 

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
      
       public DeleteGenreCommandTest(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
           
       }

       [Fact]
       public void WhenGenreIdNotFound_InvalidOperationException_ShouldBeReturn()
       {
           DeleteGenreCommand command = new DeleteGenreCommand(_Context);
           command.GenreId = 30;

           FluentActions
                   .Invoking(() => command.Handle())
                   .Should().Throw<InvalidOperationException>().And.Message
                   .Should().Be("Book type not found!");
       }

       [Fact]
       public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
       {
            DeleteGenreCommand command = new DeleteGenreCommand(_Context);
           command.GenreId = 1;

            FluentActions
                  .Invoking(() => command.Handle()).Should().NotThrow();
       }
    }
}    
 