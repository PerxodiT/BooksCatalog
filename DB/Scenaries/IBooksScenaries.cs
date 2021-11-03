using DB.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB.Scenaries
{
    public interface IBooksScenaries
    {
        Task<int> AddAuthor(int BookId, int AuthorId);
        Task<int> AddBook(AddBookVm newBook);
        Task<int> DeleteBook(int id);
        Task<List<GetAuthorVm>> GetAuthors(int id);
        Task<List<GetBookVm>> GetBook();
        Task<GetBookVm> GetBook(int id);
        Task<int> UpdateBook(int id, UpdateBookVm updBook);
    }
}