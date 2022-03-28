using System;
using Microsoft.AspNetCore.Mvc;
 using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreatAuthor
{
    public class CreateAuthorCommand 
    {
       public CreateAuthorModel Model {  get; set; }
       private readonly IBookStoreDbContext  _dbContext;
       private readonly IMapper _mapper;
       public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
       {
           _dbContext = dbContext;
           _mapper = mapper;
       }
  
       public void Handle()
       {
           var author = _dbContext.Authors.SingleOrDefault( x => x.Name == Model.Name);

           if(author is not null)
                  throw new InvalidOperationException("This author is already available.");

            author = _mapper.Map<Author>(Model);

            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
                 
       }

    }
    public class CreateAuthorModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}       