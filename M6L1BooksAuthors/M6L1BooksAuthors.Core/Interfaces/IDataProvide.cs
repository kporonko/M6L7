using M6L1BooksAuthors.Core.Models;
using M6L1BooksAuthors.Infrastructure.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6L1BooksAuthors.Core.Interfaces
{
    public interface IDataProvide
    {
        public int AddProduct(BookAdd product);

        public void UpdateProduct(BookPut product);

        public void DeleteProduct(BookDelete product);

        public Book? GetBook(int id);

        public List<Book> GetBooks();
    }
}
