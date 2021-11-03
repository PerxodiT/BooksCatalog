using Core;
using DB.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace DB.Mappers
{
    public static class BookMapper
    {
        public static Book ToEntity(this AddBookVm addBookVm, ApplicationContext _context)
        {
            if (addBookVm.Name.Length > 30) return new Book { BookId = -1 };

            var bookAuthors = _context.Authors.Where(a => addBookVm.AuthorId.Contains(a.AuthorId)).ToList();
            if (addBookVm.AuthorId.Count != bookAuthors.Count) return new Book { BookId = -1 };

            return new Book { BookId = 0, Name = addBookVm.Name, Year = addBookVm.Year, Authors = bookAuthors };
        }

        public static Book ToEntity(this UpdateBookVm updateBookVm) => new Book
        {
            BookId = updateBookVm.Name.Length > 30 ? -1 : updateBookVm.BookId,
            Name = updateBookVm.Name,
            Year = updateBookVm.Year
        };

        public static GetBookVm ToVm(this Book book)
        {
            List<GetAuthorVm> authors = new List<GetAuthorVm>();
            foreach (var author in book.Authors)
                authors.Add(author.ToVm());

            return new GetBookVm { BookId = book.BookId, BookName = book.Name, Authors = authors };
        }
    }
}
