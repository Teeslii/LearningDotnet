using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand 
    {
       private readonly BookStoreDbContext _dbContext;
       public int GenreId { get; set; }
       public UpdateGenreModel Model { get; set; }
       public UpdateGenreCommand(BookStoreDbContext dbContext)
       {
           _dbContext = dbContext;
       }
       public void Handle()
       {
           var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
           if(genre is null)
                  throw new InvalidOperationException("The type of book already exists.");
            
            if(_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                  throw new InvalidOperationException("A book genre with the same name already exists.");

            genre.Name =  string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name:  Model.Name;
            genre.IsActive = Model.IsActive;
            _dbContext.SaveChanges();      
       }
    }
    public class UpdateGenreModel
    {
           public string? Name { get; set; }
           public bool IsActive { get; set; } = true;
    } 
}       