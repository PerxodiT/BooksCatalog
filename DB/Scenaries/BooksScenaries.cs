using System.Threading.Tasks;
using DB.ViewModels;
using DB.Mappers;
using Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DB.Scenaries
{
    public class BooksScenaries : IBooksScenaries
    {
        private readonly ApplicationContext _context;
        public BooksScenaries(ApplicationContext context) =>
            _context = context;

        public async Task<int> AddBook(AddBookVm newBook)
        {
            var BookEntity = newBook.ToEntity(_context);
            if (BookEntity.BookId == -1) return (int)Errors.ValidationError;
            await _context.Books.AddAsync(BookEntity);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<GetBookVm>> GetBook()
        {
            var BooksEntityList = await _context.Books.Include(b => b.Authors).ToListAsync();
            return await Task.Run(() =>
            {
                var BooksVmList = new List<GetBookVm>();
                foreach (var Book in BooksEntityList)
                    BooksVmList.Add(Book.ToVm());
                return BooksVmList;
            });
        }

        public async Task<GetBookVm> GetBook(int id) =>
            (await _context.Books.FindAsync(id)).ToVm();

        public async Task<int> UpdateBook(int id, UpdateBookVm updBook)
        {
            if (id != updBook.BookId)
                return (int)Errors.BadRequest;
            if (!await BookExists(id))
                return (int)Errors.NotFound;

            _context.Entry(updBook.ToEntity()).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return (int)Errors.NotFound;
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAuthor(int BookId, int AuthorId)
        {
            var book = await _context.Books.Where(b => b.BookId == BookId).Include(b => b.Authors).FirstOrDefaultAsync();
            var author = await _context.Authors.FindAsync(AuthorId);
            if (book == null || author == null) return (int)Errors.NotFound;
            if (book.Authors.Contains(author)) return (int)Errors.BadRequest;
            book.Authors.Add(author);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<GetAuthorVm>> GetAuthors(int id)
        {
            var book = await _context.Books.Where(b => b.BookId == id).Include(b => b.Authors).FirstOrDefaultAsync();
            if (book == null) return new List<GetAuthorVm>();

            List<GetAuthorVm> authors = new List<GetAuthorVm>();
            foreach (var author in book.Authors)
                authors.Add(author.ToVm());

            return authors;
        }

        private async Task<bool> BookExists(int id) =>
            await _context.Books.AnyAsync(b => b.BookId == id);
    }
}
