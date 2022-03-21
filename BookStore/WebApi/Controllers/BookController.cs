using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreatBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{ 
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public BookController(BookStoreDbContext context, IMapper mapper)
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
           try
           {
               GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
               query.BookId = id;
               GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
               validator.ValidateAndThrow(query);
               result = query.Handle();
           }
           catch (Exception ex)
           {
               return BadRequest(ex.Message);
           }

           return Ok(result);
        }
        
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

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
                }*/
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            try
            {
                UpdateBookCommand  command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;

                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);

                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
     /*   [HttpGet]
        public Book? Get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }iki tane parametresiz Get metotu çalışmayacağı için yorum satırında */
    
}
