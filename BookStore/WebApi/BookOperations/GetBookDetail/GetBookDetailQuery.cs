using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using AutoMapper;

namespace  WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }

        private readonly IMapper _mapper; 
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {   
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();

            if(book is null)
               throw new InvalidOperationException("The book was not found.");
           
             BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            
            return vm;
        }

    }

    public class BookDetailViewModel
    {
         public string? Title { get; set; }
        public int PageCount { get; set; }
        public string? PublishDate { get; set; }
        public string? Genre { get; set; }
    }
}