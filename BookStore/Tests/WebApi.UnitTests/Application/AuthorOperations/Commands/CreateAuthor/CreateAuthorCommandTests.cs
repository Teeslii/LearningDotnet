using Xunit;
using TestSetup;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System;
using FluentAssertions;
using System.Linq;
using WebApi.Application.AuthorOperations.Commands.CreatAuthor;

namespace Application.AuthorOptions.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
       private readonly IMapper _mapper;
       public CreateAuthorCommandTests(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
           _mapper = textFixture.Mapper;
       }

       [Fact]
       public void WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
       {
           //arrange 
           var author = new Author()
           {
               Name = "TestName",
               Surname = "TestSurname",
               DateOfBirth = new DateTime(1923,04,21)
           };

           _Context.Authors.Add(author);
           _Context.SaveChanges();

           CreateAuthorCommand command = new CreateAuthorCommand(_Context, _mapper);
           command.Model = new CreateAuthorModel()
           {
               Name = author.Name,
               Surname= author.Surname,
               DateOfBirth = author.DateOfBirth
           };

           //act & assert 
           FluentActions
                  .Invoking(() => command.Handle())
                  .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This author is already available.");
       }

       [Fact]
       public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
       {
            CreateAuthorCommand command = new CreateAuthorCommand(_Context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                    Name = "NameCreatedTest",
                    Surname = "SurnameCreatedTest",
                    DateOfBirth = new DateTime(1943, 10, 23)
            };

             command.Model = model;
            
            // act
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();
            
          
             var author = _Context.Authors.SingleOrDefault(author => author.Name == model.Name);
           

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
       }
    }
}
