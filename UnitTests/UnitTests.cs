using LMS.Services;
using LMS.Models;
using LMS.Repositories.UnitOfWork;
using LMS.Data;
using Moq;
using LMS.DatabaseModels;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BookService_AddBook()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            var addBookVm = new AddBookViewModel()
            {
                Title = "Test",
                Author = "Test",
                Publisher = "Test",
                PublicationDate = DateTime.Now,
                ISBN = "Test",
                BookCategoryId = 1,
                TotalCopies = 1,
                AvailableCopies = 1

            };

            var dbBook = new Book()
            {
                Title = addBookVm.Title,
                Author = addBookVm.Author,
                Publisher = addBookVm.Publisher,
                PublicationDate = addBookVm.PublicationDate,
                ISBN = addBookVm.ISBN,
                BookCategoryId = addBookVm.BookCategoryId,
                TotalCopies = addBookVm.TotalCopies,
                AvailableCopies = addBookVm.AvailableCopies

            };

            var a = unitOfWork.GeneralRepository<Book>().AddAsync(dbBook);

            var result = unitOfWork.GeneralRepository<Book>()
                .Exists(b => b.Title == addBookVm.Title && b.ISBN == addBookVm.ISBN);

            if (result)
                Assert.Pass();
            else 
                Assert.Fail();
        }

        [Test]
        public void BookService_EditBook()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            var editBookVm = new EditBookViewModel()
            {
                Title = "Test Edit",
                Author = "Test ",
                Publisher = "Test",
                PublicationDate = DateTime.Now,
                ISBN = "Test Edit",
                BookCategoryId = 1,
                TotalCopies = 1,
                AvailableCopies = 1
            };

            var a = bookService.UpdateBook(editBookVm);

            var result = unitOfWork.GeneralRepository<Book>()
                .Exists(b => b.Title == editBookVm.Title && b.ISBN == editBookVm.ISBN);

            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void BookService_DeleteBook()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            var deleteBookVm = new DeleteBookViewModel() { 
                ISBN = "TestEdit"
            };

            var bookToDelete =  bookService.GetBookByISBN(deleteBookVm.ISBN);

            bookService.DeleteBook(deleteBookVm);

           

            var result = !unitOfWork.GeneralRepository<Book>().Exists(b => b.ISBN == deleteBookVm.ISBN);
            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void BookService_GetBook()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            BookService_AddBook();

            var bookToGet = bookService.GetBook(1);

            var result = bookToGet != null ? true : false;
            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void BookService_GetBookByISBN()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);
            

            var bookToGet = bookService.GetBookByISBN("Test");

            var result = bookToGet != null ? true : false;
            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }


        [Test]
        public void BookService_GetBooks()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);


            var booksToGet = bookService.GetBooks().Result;

            var result = booksToGet.Count() > 0 ? true : false;
            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }


        [Test]
        public void BookService_GetBookLeases()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);


            var booksLeasesToGet = bookService.GetBookLeases().Result;

            var result = booksLeasesToGet.Count() > 0 ? true : false;
            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void BookService_GetBooksViewModel()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);


            var bookViewModelsToGet = bookService.GetBooksViewModel().Result;

            var result = bookViewModelsToGet.Count() > 0 ? true : false;
            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void BookService_GetEditBookViewModel()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);


            var bookViewModelsToGet = bookService.GetEditBookViewModel(1).Result;

            var result = bookViewModelsToGet != null ? true : false;
            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void BookService_AddBookLease()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            var bookLeaseViewModel = new AddBookLeaseViewModel() 
            { 
                ISBN = "Test"
            };

            var result = bookService.AddBookLease(bookLeaseViewModel, "32f67ce5-c717-4bc7-be8d-9b9e52fa5431").Result;
            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void BookService_AckowledgeBookReturn()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            var result = bookService.AckowledgeBookReturn("1", "32f67ce5-c717-4bc7-be8d-9b9e52fa5431").Result;

            if (result)
                Assert.Pass();
            else
                Assert.Fail();
        }

        public void BookService_GetLeasedBooksViewModel()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            var result = bookService.GetLeasedBooksViewModel("member@example.com").Result;

            if (result.Count() > 1)
                Assert.Pass();
            else
                Assert.Fail();

        }

        public void BookService_GetBorrowedBooksViewModelByUserId()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            var result = bookService.GetBorrowedBooksViewModelByUserId("member@example.com", "32f67ce5-c717-4bc7-be8d-9b9e52fa5431").Result;

            if (result.Count() > 1)
                Assert.Pass();
            else
                Assert.Fail();
        }

        public void BookService_GetBorrowedBooksViewModel() 
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);
            var bookService = new BookService(unitOfWork);

            var result = bookService.GetBorrowedBooksViewModel().Result;

            if (result.Count() > 1)
                Assert.Pass();
            else
                Assert.Fail();

        }


        //[Test]
        //public void BookController_ViewBooks()
        //{
        //    var mockContext = new Mock<ApplicationDbContext>();
        //    var unitOfWork = new UnitOfWork(mockContext.Object);
        //    var bookService = new BookService(unitOfWork);
        //    var um = UserManager <ApplicationUser> userManager;

        //    var controller = new BookController(unitOfWork, userManager);
        //    var result = controller.ViewBooks(1) as ViewResult;
        //    Assert.AreEqual("Details", result.ViewName);

        //}

        //[Test]
        //public void AccountController_Login()
        //{
        //    var mockContext = new Mock<ApplicationDbContext>();
        //    var unitOfWork = new UnitOfWork(mockContext.Object);
        //    var bookService = new BookService(unitOfWork);
        //    var um = UserManager<ApplicationUser> userManager;

        //    var controller = new AccountController(unitOfWork, );
        //    var result = controller.Login() as ViewResult;
        //    Assert.AreEqual("Details", result.ViewName);

        //}
    }
}