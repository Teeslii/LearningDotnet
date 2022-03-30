using Xunit;
using TestSetup;
using WebApi.DBOperations;
using System;
using FluentAssertions;
using System.Linq;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace Application.AuthorOptions.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommanTestFixture>
    {
       private readonly BookStoreDbContext  _Context;
             public  UpdateAuthorCommandTest(CommanTestFixture textFixture)
       {
           _Context = textFixture.Context;
           
       }

       [Fact]
       public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
       {
           UpdateAuthorCommand command = new UpdateAuthorCommand(_Context);
            command.AuthorId = 28;

           FluentActions
                 .Invoking(() => command.Handle())
                 .Should()
                 .Throw<InvalidOperationException>()
                 .And.Message.Should().Be("The type of author already exists.");
       }

       [Fact]
       public void WhenValidBookIdAreGiven_Book_ShouldBeUpdated()
       {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_Context);
            command.AuthorId = 2;

           UpdateAuthorModel model = new UpdateAuthorModel()
           {
               Name = "Gubse",
               Surname = "Birsel"
           };

           command.Model = model;

           FluentActions
                .Invoking(() => command.Handle()).Invoke();

           var author = _Context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);

           author.Should().NotBeNull();
           author.Name.Should().Be(model.Name);
           author.Surname.Should().Be(model.Surname);   
       }

    }
}       
 