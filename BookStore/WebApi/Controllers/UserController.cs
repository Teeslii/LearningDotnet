using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using AutoMapper;
using Microsoft.Extensions.Configuration;
 
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.TokenOperations.Models;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.RefreshToken;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{ 
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly  IConfiguration _configuration;
    public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserModel newUser)
    {
         CreateUserCommand command = new CreateUserCommand(_context, _mapper);
         command.Model = newUser;
         command.Handle();

         //validator
         return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)  
    {
        CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);  
        command.Model = login;

        var token = command.Handle();   

        return token;
    }
    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)  
    {
        RefreshTokenCommand  command = new RefreshTokenCommand (_context, _configuration);  
        command.RefreshToken = token;

        var refreshToken = command.Handle();    

        return refreshToken;
    }
}   
  