using System;
using Microsoft.AspNetCore.Mvc;
 
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreatBook
{
    public class CreateBookCommand 
    {
       public CreateBookModel BookModel {  get; set; }
       private readonly BookStoreDbContext  _dbContext;
       public CreateBookCommand(BookStoreDbContext dbContext)
       {
           _dbContext = dbContext;
       }

       public void Handle()
       {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == BookModel.Title);
            if(book is not null)
                   throw new InvalidOperationException("This book is already available.");
            
            book = new Book();
            book.Title = BookModel.Title;
            book.PublishDate = BookModel.PublishDate;
            book.PageCount = BookModel.PageCount;
            book.GenreId = BookModel.GenreId;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
       }
    }
    public class CreateBookModel
    {
        public string? Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}