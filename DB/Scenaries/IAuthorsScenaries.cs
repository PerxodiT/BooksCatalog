using DB.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB.Scenaries
{
    public interface IAuthorsScenaries
    {
        Task<int> AddAuthor(AddAuthorVm newAuthor);
        Task<int> DeleteAuthor(int id);
        Task<List<GetAuthorVm>> GetAuthor();
        Task<GetAuthorVm> GetAuthor(int id);
        Task<List<GetBookVm>> GetBooks(int id);
        Task<int> UpdateAuthor(int id, UpdateAuthorVm updAuthor);
    }
}