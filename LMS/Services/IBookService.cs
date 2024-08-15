using LMS.DatabaseModels;
using LMS.Models;

namespace LMS.Services
{
    public interface IBookService
    {
        Task<bool> AddBook(AddBookViewModel addBook);
        Task<bool> UpdateBook(EditBookViewModel editBook);
        Task<bool> DeleteBook(DeleteBookViewModel deleteBook);
        Task<Book> GetBook(int itemId);
        Task<IEnumerable<Book>> GetBooks();
    }
}
