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
        public void AddProduct(BookAdd product);

        public Task UpdateProductAsync(BookPut product);

        public Task DeleteProductAsync(BookDelete product);

        public Task<Book> GetBookAsync(int id);

        public Task<List<Book>> GetBooksAsync();
    }
}
