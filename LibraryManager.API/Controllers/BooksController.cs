using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManager.API.Models;
using LibraryManager.API.Models.Dto;
using System.Web;
using Newtonsoft.Json.Linq;

namespace LibraryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        //Get("GetBookByISBN")
        [HttpGet]
        [Route("[action]/{isbn}")]
        public async Task<ActionResult<Book>> GetBookByISBN(int isbn)
        {
            var book = await _context.Books.Where(x => x.Isbn == isbn).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet()]
        [Route("[action]/{isbn}")]
        public async Task<ActionResult<bool>> CheckIfISBNIsValid(int isbn)
        {
            //int isbnNo;
            //bool success = int.TryParse(HttpUtility.UrlDecode(isbn), out isbnNo);
            //if(!success)
            //{
            //    return false;
            //}

            var result = await _context.Books.Where(x => x.Isbn == isbn).ToListAsync();

            if (result.Any())
                return true;
            return false;
        }


        //Get("GetAvailableBooks")
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAvailableBooks()
        {
            var books = _context.Books.Select(book => new BookDto
            {
                BookId = book.BookId,
                ISBN = book.Isbn,
                Title = book.Title,
                Author = book.Author,
                Category = book.Category,
                PublishedYear = book.PublishedYear,
                Publisher = book.Publisher,
                Price = book.Price,
            }
                ).ToList();

            var checkoutBooks = _context.Transactions.Where(x => x.TxnStatusId == 1 || x.TxnStatusId == 2).Select(s => new BookDto
            {
                BookId = s.Book.BookId,
                ISBN = s.Book.Isbn,
                Title = s.Book.Title,
                Author = s.Book.Author,
                Category = s.Book.Category,
                PublishedYear = s.Book.PublishedYear,
                Publisher = s.Book.Publisher,
                 Price = s.Book.Price,
            }).ToList(); 

            var resultedBooks = books.Except(checkoutBooks, new BookComparer());
            return resultedBooks.ToList();
        }

    }
}
