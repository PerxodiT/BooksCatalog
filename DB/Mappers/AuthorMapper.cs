using Core;
using DB.ViewModels;

namespace DB.Mappers
{
    public static class AuthorMapper
    {
        public static Author ToEntity(this AddAuthorVm addAuthorVm) => 
            new Author { AuthorId = 0, LastName = addAuthorVm.LastName };

        public static Author ToEntity(this UpdateAuthorVm updateAuthorVm) => 
            new Author { AuthorId = updateAuthorVm.AuthorId, LastName = updateAuthorVm.AuthorName };

        public static GetAuthorVm ToVm(this Author author) => 
            new GetAuthorVm{ AuthorId = author.AuthorId, AuthorName = author.LastName };

        public static UpdateAuthorVm ToUpdateVm(this Author author) =>
            new UpdateAuthorVm { AuthorId = author.AuthorId, AuthorName = author.LastName };

    }
}
