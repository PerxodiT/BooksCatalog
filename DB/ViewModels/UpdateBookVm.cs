using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DB.ViewModels
{
    public class UpdateBookVm
    {
        public int BookId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public int Year { get; set; }
        public List<int> AuthorId { get; set; }
    }
}
