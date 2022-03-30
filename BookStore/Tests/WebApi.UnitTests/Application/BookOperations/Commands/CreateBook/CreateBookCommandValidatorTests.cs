using System;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreatGenre;
using Xunit;
using FluentAssertions;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommanTestFixture>
    {
       [Theory]
       [InlineData("Lord Of The Rings", 0, 2, 1)]
       [InlineData("", 0, 3, 0)]
       [InlineData("", 0, 1, 1)]
       [InlineData("", 100, 2, 1)]
       [InlineData("Lor", 100, 1, 1)]
       [InlineData("Lord", 0, 2, 1)]
       [InlineData("Lord", 100, 3, 0)]
       public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int authorId, int genreId)
       {
            // arrange
            CreateBookCommand  command= new CreateBookCommand(null, null);
            command.BookModel = new CreateBookModel(){
                Title = title,  PageCount = pageCount, GenreId = genreId,  AuthorId = authorId, PublishDate = DateTime.Now.Date.AddYears(-1)}; // sürekli değişmeli

            // act 
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);   

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
       }
     
     /* Fact ile yazınca her veriyi tek tek girmek zorunda kalıyoruz ve tekrarı tam sağlayamıyoruz. 
       [Fact]
       public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
       {
            // arrange
            CreateBookCommand  command= new CreateBookCommand(null, null);
            command.BookModel = new CreateBookModel(){
                Title = "", GenreId = 0,  AuthorId = 0,  PageCount = 0, PublishDate = DateTime.Now.Date}; // sürekli değişmeli

            // act 
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);   

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
       }
       */

       [Fact]
       public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors()
       {
           CreateBookCommand command = new CreateBookCommand(null, null);
            command.BookModel = new CreateBookModel(){
                Title = "Lord Of The Rings",  PageCount = 100, GenreId = 1,  AuthorId = 2, PublishDate = DateTime.Now.Date};
                 
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);   

          
            result.Errors.Count.Should().BeGreaterThan(0);
       }
 
       [Fact]
       public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
       {
           CreateBookCommand command = new CreateBookCommand(null, null);
            command.BookModel = new CreateBookModel(){
                Title = "Lord Of The Rings",  PageCount = 100, GenreId = 1,  AuthorId = 3, PublishDate = DateTime.Now.Date.AddDays(-2)};
                 
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);   
   
            result.Errors.Count.Should().Be(0);
       }
        
    }
}        

