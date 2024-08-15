using LMS.DatabaseModels;
using LMS.Models;
using LMS.Repositories.UnitOfWork;
using System.Net;

namespace LMS.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddBookCategory(AddBookCategoryViewModel addBookCategory)
        {
            var dbBookCategory = new BookCategory
            {
                CategoryName = addBookCategory.CategoryName
            };

            await _unitOfWork.GeneralRepository<BookCategory>().AddAsync(dbBookCategory);
            _unitOfWork.Save();
        }

        public async Task DeleteBookCategory(DeleteBookCategoryViewModel deleteBookCategory)
        {
            var bookCategoryToDelete = await _unitOfWork.GeneralRepository<BookCategory>().GetByIdAsync(filter: i => i.CategoryName == deleteBookCategory.CategoryName);
            _unitOfWork.GeneralRepository<BookCategory>().Delete(bookCategoryToDelete);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<BookCategory>> GetBookCategories()
        {
            return await _unitOfWork.GeneralRepository<BookCategory>().GetAll();
        }

        public async Task<BookCategory> GetBookCategory(int itemId)
        {
            return await _unitOfWork.GeneralRepository<BookCategory>().GetByIdAsync(filter: i => i.Id == itemId);
        }

        public async Task UpdateBookCategory(EditBookCategoryViewModel editBookCategory)
        {
            var bookCategoryToUpdate = await _unitOfWork.GeneralRepository<BookCategory>().GetByIdAsync(filter: i => i.CategoryName == editBookCategory.CategoryName);
            if (bookCategoryToUpdate != null)
            {
                bookCategoryToUpdate.CategoryName = editBookCategory.CategoryName;

                _unitOfWork.Save();
            }
        }
    }
}
