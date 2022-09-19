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

        public int AddProduct(BookAdd product)
        {
            return _dataProvider.AddProduct(product);
        }

        public void UpdateProduct(BookPut product)
        {
            _dataProvider.UpdateProduct(product);
        }

        public void DeleteProduct(BookDelete product)
        {
            _dataProvider.DeleteProduct(product);
        }

        public Book? GetBook(int id)
        {
            return _dataProvider.GetBook(id);
        }

        public List<Book> GetBooks()
        {
            return _dataProvider.GetBooks();
        }
    }
}
