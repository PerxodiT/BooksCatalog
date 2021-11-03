using System.Collections.Generic;

namespace Core
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<Author> Authors { get; set; }
    }
}