using System.Threading.Tasks;
using DB.ViewModels;
using DB.Mappers;
using Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DB.Scenaries
{
    public class AuthorsScenaries : IAuthorsScenaries
    {
        private readonly ApplicationContext _context;
        public AuthorsScenaries(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddAuthor(AddAuthorVm newAuthor)
        {
            await _context.Authors.AddAsync(newAuthor.ToEntity());
            return await _context.SaveChangesAsync();
        }

        public async Task<List<GetAuthorVm>> GetAuthor()
        {
            var AuthorsEntityList = await _context.Authors.ToListAsync();
            return await Task.Run(() =>
            {
                var AuthorsVmList = new List<GetAuthorVm>();
                foreach (var Author in AuthorsEntityList)
                    AuthorsVmList.Add(Author.ToVm());
                return AuthorsVmList;
            });
        }

        public async Task<GetAuthorVm> GetAuthor(int id) =>
            (await _context.Authors.FindAsync(id)).ToVm();

        public async Task<int> UpdateAuthor(int id, UpdateAuthorVm updAuthor)
        {
            if (id != updAuthor.AuthorId)
                return (int)Errors.BadRequest;
            if (!await AuthorExists(id))
                return (int)Errors.NotFound;

            _context.Entry(updAuthor.ToEntity()).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return (int)Errors.NotFound;
            _context.Authors.Remove(author);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<GetBookVm>> GetBooks(int id)
        {
            var author = await _context.Authors.Where(a => a.AuthorId == id).Include(a => a.Books).FirstOrDefaultAsync();
            if (author == null) return new List<GetBookVm>();
            List<GetBookVm> books = new List<GetBookVm>();
            
            foreach (var book in author.Books)
                books.Add(book.ToVm());

            return books;
        }

        private async Task<bool> AuthorExists(int id) =>
            await _context.Authors.AnyAsync(a => a.AuthorId == id);
    }
}
