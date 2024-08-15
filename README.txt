---------Overview

I had to implement Identity from scratch for the first time (really fun learning experience)
I had to implement UnitTests from scratch for the first time in a web app. (failed)

I tried to split the folders inside of the LMS web app.

Unit tests do not work which is a big dissapointment - I hope we can discuss the code so you can see my understanding / rationale regardless.

I used the MVC pattern with a Service middle layer to show my understanding of it.

I used EF ASP.Net CORE for the framework, supported by a UnitOfWork and Repository pattern.

I wanted to add functionality to CRUD Book Categories (ran out of time but SQL included to populate the table.

The user uses a FK for the book list.

Didn't implement factory pattern due to the lack of variation in types (Book, BookCategory, BookLease, BookReturn) - can discuss in 3rd interview.

Missed using partial views.

Tested well via debugging regardless of UnitTest failure

Handed in as I felt I was running out of time and wanted to show it to you.

Member functionality - Login, BorrowBook, LeaseBook, ViewPersonalBorrowList, Logout
Employee functionality - Login, ViewAllBorrowedBooks, ViewBookList, AddBook, EditBook, DeleteBook ,RegisterUser, Logout
Admin functionality - Login, ViewAllBorrowedBooks, ViewBookList, AddBook, EditBook, DeleteBook ,RegisterUser, EditUser, DeleteUser logout

Had fun learning stuff.




---------Assumptions

Books Are 1 layer affair. (1 table for all types).
BookCategory are 1 layer affair. (1 table for all types).
Book Returns and Leases are linked in a 1:1 relationship.

Web UI needed to be functional and nothing more.


----------Setup

Open application.

LMS>Migrations has 20240814232623_Initial.cs and ApplicationDbContextModelSnapshot.cs

run PMC - update-database

run LMS - InitalizationScript.sql using SSMS (Users/Roles are seeded in Db Initializer class)

run application through Visual Studio

------------Logins

Admin = "admin@example.com", "!Admin173"
Employee = "employee@example.com", "!Employee173"
Member = "member@example.com","!Member173"

Any issues let me know.






