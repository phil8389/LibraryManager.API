using LibraryManager.API.Models.Dto;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManager.API.Models
{
    public class BookComparer : IEqualityComparer<BookDto>
    {
        public bool Equals(BookDto x, BookDto y)
        {
            if (x.BookId == y.BookId)
                return true;

            return false;
        }

        public int GetHashCode(BookDto obj)
        {
            return obj.BookId.GetHashCode();
        }
     
    }
}
