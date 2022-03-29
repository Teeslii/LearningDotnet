using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace  WebApi.Application.UserOperations.Commands.CreateUser
{
     public class CreateUserCommand
     {
        private readonly IMapper _mapper;
         private readonly IBookStoreDbContext  _dbContext;
         public CreateUserModel Model {get; set;}
         public CreateUserCommand(IBookStoreDbContext dbContext,   IMapper mapper)
         {
               _dbContext = dbContext;
               _mapper = mapper;
         }

         public void Handle()
         {
             var user = _dbContext.Users.SingleOrDefault( x => x.Email == Model.Email);

             if(user is not null)
                     throw new InvalidOperationException("The type of user already exists.");

             user = _mapper.Map<User>(Model);
             
             _dbContext.Users.Add(user);
             _dbContext.SaveChanges();     
         }
     }

     public class CreateUserModel
     {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

     }
}    
