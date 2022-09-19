using M6L1BooksAuthors.Core.Interfaces;
using M6L1BooksAuthors.Core.Models;
using M6L1BooksAuthors.Infrastructure.EntityFramework.Models;

namespace M6L1BooksAuthors.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IDataProvide _dataProvider;

        public BookService(IDataProvide dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void AddProduct(BookAdd product)
        {
            _dataProvider.AddProduct(product);
        }

        public async Task UpdateProductAsync(BookPut product)
        {
            _dataProvider.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(BookDelete product)
        {
            _dataProvider.DeleteProductAsync(product);
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _dataProvider.GetBookAsync(id);
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _dataProvider.GetBooksAsync();
        }
    }
}
