using WebApi.DBOperations;
using WebApi.Entities;

namespace  WebApi.Application.GenreOperations.Commands.DeleteGenre
{
     public class DeleteGenreCommand 
     {
         private readonly IBookStoreDbContext  _dbContext;
        
        public int GenreId { get; set; }
         public DeleteGenreCommand(IBookStoreDbContext dbContext)
         {
               _dbContext = dbContext;
         }

         public void Handle()
         {
             var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
             if(genre is null)
                    throw new InvalidOperationException("Book type not found!");

            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
         }

     }
}              