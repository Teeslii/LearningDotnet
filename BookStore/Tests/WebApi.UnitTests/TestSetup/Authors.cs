using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
             context.Authors.AddRange( new Author { Name = "Eric", Surname = "Ries", DateOfBirth = new DateTime(1978,09,22) }, 
                                       new Author { Name = "Charlotte", Surname = "Perkins Gilman", DateOfBirth = new DateTime(1860,06,03) }, 
                                       new Author { Name = "Frank", Surname = "Herbert", DateOfBirth = new DateTime(1986,02,11) });
        }
    }
}