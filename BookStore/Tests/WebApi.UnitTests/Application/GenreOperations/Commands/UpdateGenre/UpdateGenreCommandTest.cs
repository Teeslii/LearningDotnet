using Xunit;
using TestSetup;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;
using System.Linq;
 

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateBookCommandTest : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
      
       public UpdateBookCommandTest(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
        
       }

       [Fact]
       public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
       {
           UpdateGenreCommand command = new UpdateGenreCommand(_Context);
            command.GenreId = 0;
           

           FluentActions
                 .Invoking(() => command.Handle())
                 .Should()
                 .Throw<InvalidOperationException>()
                 .And.Message.Should().Be("The type of book already exists.");
       }

       [Fact]
       public void WhenValidGenreIdAreGiven_Book_ShouldBeUpdated()
       {
            UpdateGenreCommand command = new UpdateGenreCommand(_Context);
            command.GenreId = 3;

            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "TestGenreName"
            };
            command.Model = model;

           FluentActions
                .Invoking(() => command.Handle()).Invoke();

             var genre = _Context.Genres.FirstOrDefault(genre => genre.Name == model.Name);

            genre.Should().NotBeNull();
              
       }

    }
}       
 