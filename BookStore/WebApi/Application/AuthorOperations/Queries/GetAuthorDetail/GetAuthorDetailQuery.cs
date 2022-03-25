using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace  WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int AuthorId { get; set; }

        private readonly IMapper _mapper; 
        public GetAuthorDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.Where(query => query.Id == AuthorId).SingleOrDefault();

            if(author is null)
                 throw new InvalidOperationException("The author was not found.");

            AuthorDetailViewModel returnObj = _mapper.Map<AuthorDetailViewModel>(author);

            return returnObj;
        }
    }

    public class AuthorDetailViewModel
    {
         public string? Name { get; set; }
         public string? Surname { get; set; }
         public DateTime DateOfBirth { get; set; }
    }
}        
