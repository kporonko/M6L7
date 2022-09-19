using M6L1BooksAuthors.Core.Interfaces;
using M6L1BooksAuthors.Core.Models;
using M6L1BooksAuthors.Infrastructure.EntityFramework.Data;
using M6L1BooksAuthors.Infrastructure.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6L1BooksAuthors.Infrastructure
{
    public class DataProvider : IDataProvide
    {
        private readonly ApplicationContext _context;

        public DataProvider(ApplicationContext context)
        {
            _context = context;
        }

        public int AddProduct(BookAdd product)
        {
            try
            {
                using (_context)
                {
                    Book book = new Book { Description = product.Description, ReleaseYear = product.ReleaseYear, Title = product.Title, BookId =  GetLastId()};
                    for (int i = 0; i < product.Authors.Count; i++)
                    {
                        book.BooksAuthors.Add(new BookAuthor { Contribution = product.Authors[i].Contribution, Book = book, Author = new Author { Birthday = product.Authors[i].Birthday, FirstName = product.Authors[i].FirstName, LastName = product.Authors[i].LastName } });
                    }
                    _context.Add(book);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
            
            return GetLastId();
            
        }

        public void UpdateProduct(BookPut product)
        {
            try
            {
                using (_context)
                {
                    Book book = _context.Books.Where(x => x.BookId == product.Id).First();

                    if (book != null)
                    {
                        book.Title = product.Title;
                        book.Description = product.Description;
                        _context.Update(book);
                    }
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProduct(BookDelete product)
        {
            try
            {
                using (_context)
                {
                    Book book = _context.Books.Where(x => x.BookId == product.Id).First();
                    _context.Remove(book);
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Book GetBook(int id)
        {
            using (_context)
            {
                try
                {
                    Book book = _context.Books.Include(u => u.BooksAuthors).Where(i => i.BookId == id).FirstOrDefault();
                    if (book != null)
                    {
                        return book;
                    }

                    return null;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Book> GetBooks()
        {
            using (_context)
            {
                try
                {
                    List<Book> books = _context.Books.Include(u => u.BooksAuthors).ToList();
                    return books;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private int GetLastId()
        {
            using (_context)
            {
                if (_context.Books.ToList().Count == 0)
                {
                    return 0;
                }
                return _context.Books.ToList().Count;

            }
        }
    }
}
