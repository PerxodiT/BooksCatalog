using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Scenaries
{
    public interface IBookCatalogContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Author { get; set; }
    }
}
