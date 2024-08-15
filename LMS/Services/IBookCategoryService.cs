using LMS.DatabaseModels;
using LMS.Models;

namespace LMS.Services
{
    public interface IBookCategoryService
    {
        Task AddBookCategory(AddBookCategoryViewModel addBookCategory);
        Task UpdateBookCategory(EditBookCategoryViewModel editBookCategory);
        Task DeleteBookCategory(DeleteBookCategoryViewModel deleteBookCategory);
        Task<BookCategory> GetBookCategory(int itemId);
        Task<IEnumerable<BookCategory>> GetBookCategories();
    }
}
