using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace  WebApi.Application.UserOperations.Commands.CreateToken
{
    
        public class CreateTokenCommand 
        { 
            private readonly IBookStoreDbContext _context;
            private readonly IMapper _mapper;
            private readonly  IConfiguration _configuration;
            public CreateTokenModel Model {get; set;}
            public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
                _mapper = mapper;
            }
            public Token Handle()
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

                if(user is not null)
                {
                       TokenHandler handler = new TokenHandler(_configuration);
                       Token token = handler.CreateAccessToken(user);

                       user.RefreshToken = token.RefreshToken;
                       user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                       _context.SaveChanges();

                       return token;
                }
                else 
                  throw new InvalidOperationException("Username and password are incorrect.");
            }
        }   
        public class CreateTokenModel
        {
               public string? Email { get; set; }
               public string Password { get; set; }
        } 
} 

 
