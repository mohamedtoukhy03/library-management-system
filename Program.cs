using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using LibraryManagementSystem.Service;
using LibraryManagementSystem.UI;

// Repositories
IBookRepository bookRepo = new InMemoryBookRepository();
IMemberRepository memberRepo = new InMemoryMemberRepository();
ILoanRepository loanRepo = new InMemoryLoanRepository();

// Services
IBookService bookService = new BookService(bookRepo);
IMemberService memberService = new MemberService(memberRepo);
ILoanService loanService = new LoanService(bookRepo, memberRepo, loanRepo);

// Initial data
bookService.AddBook("Clean Code", "Uncle Bob");
bookService.AddBook("Design Patterns", "Erich Gamma");

memberService.RegisterMember("Mohamed", MemberType.VIP);
memberService.RegisterMember("Mahmoud", MemberType.Regular);

// Run console application
var consoleMenu = new ConsoleMenu(bookService, memberService, loanService);
consoleMenu.Run();
