using M6L1BooksAuthors.Core.Interfaces;
using M6L1BooksAuthors.Core.Models;
using M6L1BooksAuthors.Infrastructure.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6L1BooksAuthors.Core.Tests
{
    internal class MockDataProvider : IDataProvide
    {
        public MockDataProvider()
        {

        }

        public MockDataProvider(List<Book> books)
        {
            data = books;
        }
        public List<Book> data = new List<Book>();
        public int AddProduct(BookAdd product)
        {
            Book newBook = new Book { BookId = GetLastId() + 1, Title = product.Title, Description = product.Description, ReleaseYear = product.ReleaseYear };
            if (product.Title == null || product.Description == null || product.ReleaseYear == null)
            {
                return -1;
            }
            for (int i = 0; i < product.Authors.Count; i++)
            {
                newBook.BooksAuthors.Add(new BookAuthor { Contribution = product.Authors[i].Contribution, Book = newBook, Author = new Author { Birthday = product.Authors[i].Birthday, FirstName = product.Authors[i].FirstName, LastName = product.Authors[i].LastName } });
            }
            data.Add(newBook);


            return data.Count;
        }

        public void DeleteProduct(BookDelete product)
        {
            Book? bookToDelete = data.FirstOrDefault(x => x.BookId == product.Id);
            data.Remove(bookToDelete);
        }

        public Book? GetBook(int id)
        {
            Book? book = data.FirstOrDefault(x => x.BookId == id);
            return book;
        }

        public List<Book> GetBooks()
        {
            return data;
        }

        public void UpdateProduct(BookPut product)
        {
            var book = data.FirstOrDefault(x => x.BookId == product.Id);
            book.Title = product.Title;
            book.Description = product.Description;
        }

        private int GetLastId()
        {
            if (data.Count == 0)
                return 0;
            
            return data.Count;
        } 
    }
}
