using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;

namespace  WebApi.Application.GenreOperations.Queries.GetGenreDetail 
{
    public class GetGenreDetailQuery
    {
        public int GenreId {get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetGenreDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public  GenreDetailViewModel  Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
             if(genre is null)
                    throw new InvalidOperationException("Book type not found!");
              
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }      
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }  
}    