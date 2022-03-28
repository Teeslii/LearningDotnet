using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;

namespace TestSetup
{
   public class CommanTestFixture
   {
        public BookStoreDbContext Context {get; set;}
        public IMapper Mapper {get; set;}

        public CommanTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
   }
}