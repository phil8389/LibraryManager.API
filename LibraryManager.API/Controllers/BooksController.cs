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

        // GET: api/Books/5
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

            // var books = _context.Books.con;
           // var checkoutBooks = _context.Transactions.Where(x => x.TxnStatusId == 1 || x.TxnStatusId == 2).Select(x=>x.Book);

            var resultedBooks = books.Except(checkoutBooks, new BookComparer());
            return resultedBooks.ToList();

            // var res = books.Except()


            //var result = from book in _context.Books
            //             join txn in _context.Transactions on book.BookId equals txn.BookId into Txns
            //             from m in Txns.DefaultIfEmpty()
            //                 // where m.TxnStatusId != 1 
            //             select new BookDto
            //             {
            //                 BookId = book.BookId,
            //                 ISBN = book.Isbn,
            //                 Title = book.Title,
            //                 Author = book.Author,
            //             };

            //var result = from txn in _context.Transactions
            //             join book in _context.Books on   txn.BookId equals book.BookId into Txns
            //             from m in Txns.DefaultIfEmpty()
            //                 // where m.TxnStatusId != 1 
            //             select new BookDto
            //             {
            //                 BookId = m.BookId,
            //                 ISBN = m.Isbn,
            //                 Titile = m.Title,
            //                 Author = m.Author,
            //             };




            //return await _context.Books.Where(x => x.Transactions. == 1).OrderByDescending(x => x.CheckoutDate).Select(s => new CheckoutItem
            //{
            //    TxnId = s.TxnId,
            //    BookId = s.BookId,
            //    BookTitile = s.Book.Title,
            //    Author = s.Book.Author,
            //    UserId = s.UserId,
            //    UserFullName = s.User.FullName,
            //    CheckoutDate = s.CheckoutDate,
            //    DueDate = s.DueDate,
            //    ISBN = s.Book.Isbn,
            //    LastRenewedDate = s.LastRenewedDate
            //}).ToListAsync();
        }


        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
