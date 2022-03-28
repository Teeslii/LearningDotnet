using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;

namespace  WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _dbContext.Authors.OrderBy(x => x.Id).ToList<Author>(); // kitaba göre tekrar bak

            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(authors);
            
            return returnObj;
        }
    }     
    public class AuthorsViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }   
}    