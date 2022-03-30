using Xunit;
using TestSetup;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System;
using FluentAssertions;
using System.Linq;
using WebApi.Application.GenreOperations.Commands.CreateGenra;

namespace Application.BookOperations.Commands.CreatGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
       private readonly IMapper _mapper;
       public CreateGenreCommandTests(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
           _mapper = textFixture.Mapper;
       }

       [Fact]
       public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
       {
           //arrange 
           var genre = new Genre()
            {
                Name ="addedGenre"
            };

           _Context.Genres.Add(genre);
           _Context.SaveChanges();

           CreateGenreCommand command = new CreateGenreCommand(_Context);
           command.Model = new CreateGenreModel(){Name = genre.Name};

           //act & assert 
           FluentActions
                  .Invoking(() => command.Handle())
                  .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The type of book already exists.");
       }

     [Fact]
       public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
       {
            CreateGenreCommand command = new CreateGenreCommand(_Context);
            CreateGenreModel model = new CreateGenreModel()
            {
                   Name ="addedGenreNames"
            };
            command.Model = model;
            
            // act
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();
            
            // assert
            var genre = _Context.Genres.SingleOrDefault(genre => genre.Name == model.Name);

            genre.Should().NotBeNull(); 
       }
    }  
}
 