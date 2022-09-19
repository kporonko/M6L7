using M6L1BooksAuthors.Core.Interfaces;
using M6L1BooksAuthors.Core.Models;
using M6L1BooksAuthors.Core.Services;
using M6L1BooksAuthors.Infrastructure.EntityFramework.Models;
using Moq;
using Xunit;

namespace M6L1BooksAuthors.Core.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public void AddProduct_ValidProduct_AddedToDb()
        {
            // Arrange
            //var service = new BookService(new MockDataProvider());
            IDataProvide provider = Mock.Of<IDataProvide>(p => p.AddProduct(It.IsAny<BookAdd>()) == 1);
            var service = new BookService(provider);

            // Act
            int res = service.AddProduct(new Models.BookAdd { Authors = new List<AuthorAdd>(), ReleaseYear = 1999, Description = "aaa", Title = "Book" });
            var books = service.GetBooks();

            // Assert
            Assert.Equal(1, res);
        }

        [Fact]
        public void AddProduct_InvalidProduct_NotAdded()
        {
            // Arrange
            IDataProvide provider = Mock.Of<IDataProvide>(p => p.AddProduct(It.IsAny<BookAdd>()) == -1);
            //var service = new BookService(new MockDataProvider());
            var service = new BookService(provider);

            // Act
            int res = service.AddProduct(new Models.BookAdd { Authors = new List<AuthorAdd>(), ReleaseYear = 1999, Title = "Book" });

            // Assert
            Assert.Equal(-1, res);
        }

        [Fact]
        public void GetProduct_NonExistingProduct_NoProduct()
        {
            // Arrange
            var service = new BookService(new MockDataProvider());

            // Act
            var res = service.GetBook(1);
            
            // Assert
            Assert.Null(res);
        }

        [Fact]
        public void GetProduct_ValidProduct_ProductWithCorrectId()
        {
            // Arrange
            var service = new BookService(new MockDataProvider());
            _ = service.AddProduct(new Models.BookAdd { Authors = new List<AuthorAdd>(), ReleaseYear = 1999, Description = "aaa", Title = "Book" });

            // Act
            var res = service.GetBook(1);

            // Assert

            Assert.Equal(1, res.BookId);
        }

        [Fact]
        public void DeleteProduct_ValidProduct_NoProductInStorage()
        {
            // Arrange
            var service = new BookService(new MockDataProvider(new List<Infrastructure.EntityFramework.Models.Book>() { new Infrastructure.EntityFramework.Models.Book { BookId = 1, BooksAuthors = new List<BookAuthor>(), Description = "", ReleaseYear = 11, Title = "" } }));

            // Act
            service.DeleteProduct(new BookDelete() { Id = 1});

            // Assert
            var res = service.GetBook(1);
            Assert.Null(res);
        }

        [Fact]
        public void DeleteProduct_ValidProductAndNoSuchProductInStorage_NothingHappens()
        {
            // Arrange
            var service = new BookService(new MockDataProvider());

            // Act
            service.DeleteProduct(new BookDelete() { Id = 1 });

            // Assert
            var res = service.GetBook(1);
            Assert.Null(res);
        }

        [Fact]
        public void GetAllProducts_NoProductsInStorage_ReturnsEmptyList()
        {
            // Arrange
            var service = new BookService(new MockDataProvider());

            // Act
            var books = service.GetBooks();

            // Assert
            Assert.Empty(books);
        }

        [Fact]
        public void GetAllProducts_OneProductInStorage_ReturnsListOf1Product()
        {
            // Arrange
            var service = new BookService(new MockDataProvider());
            service.AddProduct(new Models.BookAdd { Authors = new List<AuthorAdd>(), ReleaseYear = 1999, Description = "aaa", Title = "Book" });

            // Act
            var books = service.GetBooks();

            // Assert
            Assert.Single(books);
        }

        [Fact]
        public void UpdateProduct_ValidProduct_UpdatedProduct()
        {
            // Arrange
            var service = new BookService(new MockDataProvider());
            service.AddProduct(new Models.BookAdd { Authors = new List<AuthorAdd>(), ReleaseYear = 1999, Description = "initial", Title = "initial" });

            // Act
            service.UpdateProduct(new BookPut() { Id = 1, Description = "updated", Title = "updated" });
            var bookAfterUpdateMethod = service.GetBook(1);

            // Assert
            Assert.Equal("updated", bookAfterUpdateMethod.Description);
            Assert.Equal("updated", bookAfterUpdateMethod.Title);
        }
    }
}