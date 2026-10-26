using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using LibraryManagementSystem.Service;
using LibraryManagementSystem.UI;
using LibraryManagementSystem.Validation;

// Repositories
IBookRepository bookRepo = new InMemoryBookRepository();
IMemberRepository memberRepo = new InMemoryMemberRepository();
ILoanRepository loanRepo = new InMemoryLoanRepository();

// Validators
IBookValidator bookValidator = new BookValidator();
IMemberValidator memberValidator = new MemberValidator();
IBorrowOperationValidator borrowValidator = new BorrowOperationValidator();

// Services
IBookService bookService = new BookService(bookRepo, bookValidator);
IMemberService memberService = new MemberService(memberRepo, memberValidator);
ILoanService loanService = new LoanService(bookRepo, memberRepo, loanRepo, borrowValidator);

// Initial data
bookService.AddBook("Clean Code", "Uncle Bob");
bookService.AddBook("Design Patterns", "Erich Gamma");

memberService.RegisterMember("Mohamed", MemberType.VIP);
memberService.RegisterMember("Mahmoud", MemberType.Regular);

// Run console application
var consoleMenu = new ConsoleMenu(bookService, memberService, loanService);
consoleMenu.Run();
