using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace  WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
     public class DeleteAuthorCommand 
     {
         private readonly IBookStoreDbContext  _dbContext;
        
        public int AuthorId { get; set; }
         public DeleteAuthorCommand(IBookStoreDbContext dbContext)
         {
               _dbContext = dbContext;
         }

                
         public void Handle()
         {
             var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);

             if(author is null)
                throw new InvalidOperationException("Author not found!");

            if(_dbContext.Books.Include(x => x.Author).Any(command => command.Author.Id == AuthorId))
                     throw new InvalidOperationException("The author cannot be deleted without deleting the book.");

             _dbContext.Authors.Remove(author);
             _dbContext.SaveChanges(); 
         }
    }
}          
