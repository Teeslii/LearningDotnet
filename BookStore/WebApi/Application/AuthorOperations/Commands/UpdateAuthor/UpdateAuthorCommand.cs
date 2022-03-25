using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand 
    {
       private readonly BookStoreDbContext _dbContext;
       public int AuthorId { get; set; }
       public UpdateAuthorModel Model { get; set; }
       public  UpdateAuthorCommand(BookStoreDbContext dbContext)
       {
           _dbContext = dbContext;
       }

       public void Handle()
       {
           var author =_dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);

           if(author is null)
                   throw new InvalidOperationException("The type of author already exists.");

           if(_dbContext.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != AuthorId)) 
                throw new InvalidOperationException("A author with the same name already exists.");     

            author.Name = string.IsNullOrEmpty(Model.Name) ? author.Name: Model.Name;

            author.Surname = string.IsNullOrEmpty(Model.Surname) ? author.Surname: Model.Surname;

            _dbContext.SaveChanges(); 
       }
    }

    public class  UpdateAuthorModel
    {
         public string? Name { get; set; }
         public string? Surname { get; set; }

    }
}    
