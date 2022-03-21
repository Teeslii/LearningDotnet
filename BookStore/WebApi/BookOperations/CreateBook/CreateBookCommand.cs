using System;
using Microsoft.AspNetCore.Mvc;
 using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreatBook
{
    public class CreateBookCommand 
    {
       public CreateBookModel BookModel {  get; set; }
       private readonly BookStoreDbContext  _dbContext;
       private readonly IMapper _mapper;
       public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
       {
           _dbContext = dbContext;
           _mapper = mapper;
       }

       public void Handle()
       {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == BookModel.Title);
            if(book is not null)
                   throw new InvalidOperationException("This book is already available.");
            
            book = _mapper.Map<Book>(BookModel);
         

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