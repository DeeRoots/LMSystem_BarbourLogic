using LMS.DatabaseModels;
using LMS.Models;
using LMS.Repositories;
using LMS.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LMS.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddBook(AddBookViewModel addBook)
        {
            var dbBook = new Book
            {
                Title = addBook.Title,
                Author = addBook.Author,
                Publisher = addBook.Publisher,
                PublicationDate = addBook.PublicationDate,
                ISBN = addBook.ISBN,
                BookCategoryId = addBook.BookCategoryId,
                TotalCopies = addBook.TotalCopies,
                AvailableCopies = addBook.AvailableCopies
            };


            await _unitOfWork.GeneralRepository<Book>().AddAsync(dbBook);
            _unitOfWork.Save();

            return _unitOfWork.GeneralRepository<Book>().Exists(b => b.Title == addBook.Title && b.ISBN == addBook.ISBN);
        }

        public async Task<bool> DeleteBook(DeleteBookViewModel deleteBook)
        {
            var bookToDelete = await GetBookByISBN(deleteBook.ISBN);

            if (bookToDelete != null)
            {
                _unitOfWork.GeneralRepository<Book>().Delete(bookToDelete);
                _unitOfWork.Save();
            }

            return !_unitOfWork.GeneralRepository<Book>().Exists(b => b.ISBN == bookToDelete.ISBN);
        }

        public async Task<bool> UpdateBook(EditBookViewModel editBook)
        {
            var books = await _unitOfWork.GeneralRepository<Book>().GetAll();
            var bookToUpdate = books.FirstOrDefault(b => b.ISBN == editBook.OriginalISBN);

            if (bookToUpdate != null)
            {
                bookToUpdate.Title = editBook.Title;
                bookToUpdate.Author = editBook.Author;
                bookToUpdate.Publisher = editBook.Publisher;
                bookToUpdate.PublicationDate = editBook.PublicationDate;
                bookToUpdate.ISBN = editBook.ISBN;
                bookToUpdate.BookCategoryId = editBook.BookCategoryId;
                bookToUpdate.TotalCopies = editBook.TotalCopies;
                bookToUpdate.AvailableCopies = editBook.AvailableCopies;

                _unitOfWork.GeneralRepository<Book>().Update(bookToUpdate);
                _unitOfWork.Save();
            }
            return _unitOfWork.GeneralRepository<Book>().Exists(b => b.ISBN == editBook.ISBN);
        }

        public async Task<Book> GetBook(int bookId)
        {
            return await _unitOfWork.GeneralRepository<Book>().GetByIdAsync(filter: i => i.Id == bookId);
        }

        public async Task<Book> GetBookByISBN(string ISBN)
        {
            var books = await _unitOfWork.GeneralRepository<Book>().GetAll();

            var book = books.FirstOrDefault(b => b.ISBN == ISBN);
            if (book == null)
                return book;
            else
                return new Book()
                {
                    Title = "Null",
                    Author = "Null",
                    Publisher = "Null",
                    ISBN = "Null",
                    PublicationDate = DateTime.UtcNow
                };
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _unitOfWork.GeneralRepository<Book>().GetAll();
        }

        public async Task<IEnumerable<BookLease>> GetBookLeases()
        {
            return await _unitOfWork.GeneralRepository<BookLease>().GetAll();
        }

        public async Task<List<ViewBookViewModel>> GetBooksViewModel()
        {
            var vmBooks = new List<ViewBookViewModel>();
            var books = await GetBooks();
            var vmBook = new ViewBookViewModel();

            if (books.Count() > 0)
            {
                foreach (var book in books)
                {
                    vmBook = new ViewBookViewModel
                    {
                        Title = book.Title,
                        Author = book.Author,
                        Publisher = book.Publisher,
                        PublicationDate = book.PublicationDate.ToString("dd/MM/yyyy HH:mm:ss"),
                        CategoryName = _unitOfWork.GeneralRepository<BookCategory>().GetById(book.BookCategoryId).CategoryName,
                        ISBN = book.ISBN,
                        TotalCopies = book.TotalCopies.ToString(),
                        AvailableCopies = book.AvailableCopies.ToString()
                    };

                    vmBooks.Add(vmBook);
                }
            }            
                
            return vmBooks;
        }

        public async Task<EditBookViewModel> GetEditBookViewModel(int Id)
        {
            var vmBook = new EditBookViewModel();
            var book = await GetBook(Id);

            if (book != null)
            {
                vmBook = new EditBookViewModel
                {
                    Title = book.Title,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    PublicationDate = book.PublicationDate,
                    BookCategoryId = book.BookCategoryId,
                    ISBN = book.ISBN,
                    OriginalISBN = book.ISBN,
                    TotalCopies = book.TotalCopies,
                    AvailableCopies = book.AvailableCopies
                };
            }
            return vmBook;
        }

        public async Task<bool> AddBookLease(AddBookLeaseViewModel addBookLeaseViewModel, string userId)
        {
            var book = await GetBookByISBN(addBookLeaseViewModel.ISBN);

            var bookLease = new BookLease()
            {
                BookId = book.Id,
                UserLeasingId = userId,
                LeasedOn = DateTime.Now,
                ReturnBy = DateTime.Now.AddDays(28),

            };

            book.AvailableCopies -= 1;


            await _unitOfWork.GeneralRepository<BookLease>().AddAsync(bookLease);
            await _unitOfWork.GeneralRepository<Book>().UpdateAsync(book);
            _unitOfWork.Save();

            return _unitOfWork.GeneralRepository<BookLease>().Exists(b => b.BookId == book.Id && b.LeasedOn == bookLease.LeasedOn);
        }

        public async Task<bool> AckowledgeBookReturn(string bookLeaseId, string userId)
        {
            var bookLease = _unitOfWork.GeneralRepository<BookLease>().GetById(int.Parse(bookLeaseId));
            if (bookLease != null)
            {
                bookLease.Returned = true;
            }            

            var book = _unitOfWork.GeneralRepository<Book>().GetById(bookLease.BookId);

            book.AvailableCopies += 1;

            var bookReturn = new BookReturn()
            {
                BookId = bookLease.BookId,
                BookLeaseId = int.Parse(bookLeaseId),
                UserLeasingId = userId,
                ReturnedOn = DateTime.Now
            };


            await _unitOfWork.GeneralRepository<BookReturn>().AddAsync(bookReturn);
            await _unitOfWork.GeneralRepository<BookLease>().UpdateAsync(bookLease);
            _unitOfWork.Save();

            return _unitOfWork.GeneralRepository<BookReturn>().Exists(b => b.BookId == bookLease.BookId && b.UserLeasingId == userId);
        }

        public async Task<List<LeasedBookViewModel>> GetLeasedBooksViewModel(string userName)
        {
            var vmBooks = new List<LeasedBookViewModel>();
            var bookLeases = await GetBookLeases();

            foreach (var lease in bookLeases.Where(r => r.Returned == false))
            {
                var vmLeasedBook = new LeasedBookViewModel
                {
                    BookLeaseId = lease.Id.ToString(),
                    BookName = GetBook(lease.BookId).Result.Title,
                    UserLeasedName = userName,
                    LeasedOn = lease.LeasedOn.ToString("dd/MM/yyyy"),
                    ReturnBy = lease.ReturnBy.ToString("dd/MM/yyyy"),
                    Overdue = DateTime.Now > lease.ReturnBy ? true : false
                };

                vmBooks.Add(vmLeasedBook);
            }

            return vmBooks;
        }

        public async Task<List<LeasedBookViewModel>> GetBorrowedBooksViewModelByUserId(string userName, string userId)
        {
            var vmBookLeases = new List<LeasedBookViewModel>();
            var bookLeases = await GetBookLeases();

            foreach (var lease in bookLeases.Where(l => l.UserLeasingId == userId))
            {
                var vmLeasedBook = new LeasedBookViewModel
                {
                    BookLeaseId = lease.Id.ToString(),
                    BookName = GetBook(lease.BookId).Result.Title,
                    UserLeasedName = $"{userName}",
                    LeasedOn = lease.LeasedOn.ToString("dd/MM/yyyy"),
                    ReturnBy = lease.ReturnBy.ToString("dd/MM/yyyy"),
                    Overdue = DateTime.Now > lease.ReturnBy ? true : false
                };

                vmBookLeases.Add(vmLeasedBook);
            }

            return vmBookLeases;
        }

        public async Task<List<LeasedBookViewModel>> GetBorrowedBooksViewModel()
        {
            var vmBookLeases = new List<LeasedBookViewModel>();
            var bookLeases = await GetBookLeases();

            foreach (var lease in bookLeases)
            {
                var vmLeasedBook = new LeasedBookViewModel
                {
                    BookLeaseId = lease.Id.ToString(),
                    BookName = GetBook(lease.BookId).Result.Title,
                    UserLeasedName = $"{lease.UserLeasingId}".Substring(0, 8) + "Name",
                    LeasedOn = lease.LeasedOn.ToString("dd/MM/yyyy"),
                    ReturnBy = lease.ReturnBy.ToString("dd/MM/yyyy")
                };

                vmBookLeases.Add(vmLeasedBook);
            }

            return vmBookLeases;
        }
    }
}
