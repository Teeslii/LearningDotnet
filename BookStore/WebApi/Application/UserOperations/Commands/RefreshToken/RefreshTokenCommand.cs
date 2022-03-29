using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace  WebApi.Application.UserOperations.Commands.RefreshToken
{
    
        public class RefreshTokenCommand 
        { 
            private readonly IBookStoreDbContext _context;
             public string RefreshToken { get; set; }
            private readonly  IConfiguration _configuration; 
            public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
                
            }
            public Token Handle()
            {
                var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

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
                  throw new InvalidOperationException("A Valid Refresh Token was not found!");
            }
        }   
}        