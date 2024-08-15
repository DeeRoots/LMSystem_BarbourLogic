using LMS.DatabaseModels;
using LMS.Models;
using LMS.Repositories.UnitOfWork;
using LMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace LMS.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;   
            _userManager = userManager;
           
        }

        public async Task<IActionResult> ViewBooks()
        {
            var bookService = new BookService(_unitOfWork);
            var booksVm = new ViewBooksViewModel() { Books = await bookService.GetBooksViewModel() };
           
            return View(booksVm);
        }

        public async Task<IActionResult> ViewBorrowedBooks()
        {
            var bookService = new BookService(_unitOfWork);
            var booksVm = new ViewLeasedBooksViewModel() { LeasedBooks = await bookService.GetBorrowedBooksViewModel() };

            return View(booksVm);
        }

        public async Task<IActionResult> ViewBorrowedBooksByUserId()
        {
            var bookService = new BookService(_unitOfWork);
            var booksVm = new ViewLeasedBooksViewModelByUserIdViewModel() { LeasedBooks = await bookService.GetBorrowedBooksViewModelByUserId(User.FindFirstValue(ClaimTypes.Name), User.FindFirstValue(ClaimTypes.NameIdentifier)) };

            return View(booksVm);
        }


        public async Task<IActionResult> AddBook()
        {
            var bookCategoryService = new BookCategoryService(_unitOfWork);
            var addBookViewModel = new AddBookViewModel();
            addBookViewModel.PublicationDate = DateTime.Now;

            var dbBookCategories = await bookCategoryService.GetBookCategories();

            foreach (var category in dbBookCategories) {
                addBookViewModel.BookCategories.Add(new SelectListItem(category.CategoryName, category.Id.ToString()));
            }
            
            return View(addBookViewModel);
        }

        public async Task<IActionResult> ProcessAddBook(AddBookViewModel addBookViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookService = new BookService(_unitOfWork);

                var success = await bookService.AddBook(addBookViewModel);
                if (success)
                    return RedirectToAction("ViewBooks", "Book");
                else
                    return RedirectToAction("AddBook", "Book");

            }
            else              
                return RedirectToAction("Index","Home");
        }

       
        public async Task<IActionResult> EditBook(string ISBN)
        {
            var bookService = new BookService(_unitOfWork);
            var bookCategoryService = new BookCategoryService(_unitOfWork);
                    

            var dbBook = await bookService.GetBookByISBN(ISBN);
            var dbBookCategories = await bookCategoryService.GetBookCategories();

            var editBookViewModel = await bookService.GetEditBookViewModel(dbBook.Id);

            foreach (var category in dbBookCategories)
            {
                if (category.Id == dbBook.BookCategoryId)
                    editBookViewModel.BookCategories.Add(new SelectListItem(category.CategoryName, category.Id.ToString(), selected:true));
                else
                    editBookViewModel.BookCategories.Add(new SelectListItem(category.CategoryName, category.Id.ToString()));
            }         


            return View(editBookViewModel);
        }
        public async Task<IActionResult> ProcessEditBook(EditBookViewModel editBookViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookService = new BookService(_unitOfWork);

                var success = await bookService.UpdateBook(editBookViewModel);
                if (success)
                    return RedirectToAction("ViewBooks", "Book");
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return RedirectToAction("EditBook","Book",new {ISBN = editBookViewModel.OriginalISBN});
                }

            }
            else
                return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteBook(string Isbn, bool confirmDelete)
        {
            var deleteBookVM = new DeleteBookViewModel() {ISBN = Isbn };
            return View(deleteBookVM);
        }

        public async Task<IActionResult> ProcessDeleteBook(DeleteBookViewModel deleteBookViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookService = new BookService(_unitOfWork);

                var success = await bookService.DeleteBook(deleteBookViewModel);
                if (success)
                    return RedirectToAction("ViewBooks", "Book");
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return RedirectToAction("DeleteBook", "Book", new { ISBN = deleteBookViewModel.ISBN });
                }

            }
            else
                return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LeaseBook()
        {
            var bookService = new BookService(_unitOfWork);
            var leaseBookVm = new LeaseBookViewModel() { Books = await bookService.GetBooksViewModel() };

            return View(leaseBookVm);
  
        }

        public async Task<IActionResult> ProcessLeaseBook(AddBookLeaseViewModel addBookLeaseViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookService = new BookService(_unitOfWork);             
                  

                var success = await bookService.AddBookLease(addBookLeaseViewModel, User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (success)
                    return RedirectToAction("LeaseBook", "Book");
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Lease Attempt");
                    return RedirectToAction("LeaseBook", "Book");
                }

            }
            else
                return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ReturnBook()
        {
            var bookService = new BookService(_unitOfWork);
            var booksVm = new ReturnBookViewModel() { LeasedBooks = await bookService.GetLeasedBooksViewModel(User.FindFirstValue(ClaimTypes.Name)) };

            return View(booksVm);
        }

        public async Task<IActionResult> ProcessReturnBook(ProcessReturnBookViewModel processReturnBookViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookService = new BookService(_unitOfWork);             
                  

                var success = await bookService.AckowledgeBookReturn(processReturnBookViewModel.BookLeaseId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (success)
                    return RedirectToAction("ReturnBook", "Book");
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Lease Attempt");
                    return RedirectToAction("ReturnBook", "Book");
                }

            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}
