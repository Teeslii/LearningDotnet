using WebApi.DBOperations;
using WebApi.Entities;

namespace  WebApi.Application.GenreOperations.Commands.CreateGenra
{
     public class CreateGenreCommand
     {
         private readonly BookStoreDbContext  _dbContext;
         public CreateGenreModel Model {get; set;}
         public CreateGenreCommand(BookStoreDbContext dbContext)
         {
               _dbContext = dbContext;
         }

         public void Handle()
         {
             var genre = _dbContext.Genres.SingleOrDefault( x => x.Name == Model.Name);
             if(genre is not null)
                     throw new InvalidOperationException("The type of book already exists.");

             genre = new Genre();   
             genre.Name = Model.Name;
             _dbContext.Genres.Add(genre);
             _dbContext.SaveChanges();     
         }
     }

     public class CreateGenreModel
     {
         public string? Name { get; set; }
     }
}    
