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

namespace LibraryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public TransactionsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        //Get("GetCheckoutItems")
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<CheckoutDto>>> GetCheckoutItems()
        {
            return await _context.Transactions.Where(x => x.TxnStatusId == 1 || x.TxnStatusId == 2).Select(s => new CheckoutDto
            {
                TxnId = s.TxnId,
                BookId = s.BookId,
                ISBN = s.Book.Isbn,
                BookTitle = s.Book.Title,
                Author = s.Book.Author,
                UserId = s.UserId,
                UserFullName = s.User.FullName,
                CheckoutDate = s.CheckoutDate,
                DueDate = s.DueDate,               
                LastRenewedDate = s.LastRenewedDate,
                RenewalCount = s.RenewalCount
            }).OrderByDescending(x => x.CheckoutDate).ToListAsync();
        }

        [HttpGet()]
        [Route("[action]/{isbn}/{bookid}")]
        public async Task<ActionResult<bool>> CheckIfBookIsAlreadyOut(int isbn, int bookid)
        {
            var result = await _context.Transactions.Where(x => x.Book.Isbn == isbn
            && (x.TxnStatusId == 1 || x.TxnStatusId == 2)).ToListAsync();

            if (result.Any())
                return true;
            return false;
        }

        //[HttpGet()]
        //[Route("[action]/{isbn}")]
        //public async Task<ActionResult<bool>> CheckIfBookCanBeRenewed(int isbn)
        //{
        //    //   var result = await _context.Transactions.Where(x => x.Book.Isbn == isbn
        //    //&& (x.TxnStatusId == 1 || x.TxnStatusId == 2)).ToListAsync();

        //    //   if (result.Any())
        //    //       return true;
        //    //   return false;

        //    var txn = await _context.Transactions.Where(x => x.Book.Isbn == isbn && (x.TxnStatusId == 1 || x.TxnStatusId == 2)).FirstOrDefaultAsync();
        //    if (txn == null) return false;
        //    if (txn.RenewalCount >= 6)
        //    {
        //        return false;
        //    }

        //    return true;

        //}

        [HttpGet()]
        [Route("[action]/{isbn}")]
        public async Task<ActionResult<Transaction>> CheckIfBookCanBeRenewed(int isbn)
        {          

            return await _context.Transactions.Where(x => x.Book.Isbn == isbn && (x.TxnStatusId == 1 || x.TxnStatusId == 2)).FirstOrDefaultAsync();
          

        }

        [HttpGet()]
        [Route("[action]/{isbn}")]
        public async Task<ActionResult<Transaction>> GetActiveTransaction(int isbn)
        {
            return await _context.Transactions.Where(x => x.Book.Isbn == isbn && (x.TxnStatusId == 1 || x.TxnStatusId == 2)).FirstOrDefaultAsync();


        }


        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.TxnId)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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



        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.TxnId }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TxnId == id);
        }
    }
}
