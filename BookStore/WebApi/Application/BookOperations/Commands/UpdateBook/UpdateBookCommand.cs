using System;
using Microsoft.AspNetCore.Mvc;
 
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand 
    {
       private readonly IBookStoreDbContext _dbContext;
       public int BookId { get; set; }
       public UpdateBookModel Model { get; set; }
       public UpdateBookCommand(IBookStoreDbContext dbContext)
       {
           _dbContext = dbContext;
       }

       public void Handle()
       {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if(book is null)
               throw new InvalidOperationException("The book to be updated could not be found.");
               
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            
            _dbContext.SaveChanges();

       }
    }
    public class UpdateBookModel
    {
        public string? Title { get; set; }
        public int GenreId { get; set; }
    }

}