using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Commands.CreatBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{ 
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    public BookController(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
  
        [HttpGet]
        public IActionResult GetBooks()
        {
           GetBooksQuery query = new GetBooksQuery(_context, _mapper);
           var result = query.Handle();

           return Ok(result);
        } 

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
           BookDetailViewModel result;
            
               GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
               query.BookId = id;
               GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
               validator.ValidateAndThrow(query);
               result = query.Handle();
          

           return Ok(result);
        }
        
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
                CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                command.BookModel = newBook;
                CreateBookCommandValidator  validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
/*      Hata yakalama olayını Custom exception middleware'e taşındı. 
            try
            {
                command.BookModel = newBook;
                CreateBookCommandValidator  validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
 
               /* using FluentValidation.Results; bununun için kullandık.
                if(!result.IsValid)
                {

                   foreach (var item in result.Errors)
                    {
                        Console.WriteLine("Feature: "+ item.PropertyName + "- Error Message:" + item.ErrorMessage);
                    } 
                }
                else
                {
                     command.Handle();
                }     // bu kısıma kadar
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
            */
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
             
                UpdateBookCommand  command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;

                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
             
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);

                command.Handle();
             
                return Ok();
        }
     /*   [HttpGet]
        public Book? Get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }iki tane parametresiz Get metotu çalışmayacağı için yorum satırında */
    
}
