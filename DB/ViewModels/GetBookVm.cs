using System.Collections.Generic;

namespace DB.ViewModels
{
    public class GetBookVm
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public List<GetAuthorVm> Authors { get; set; }
    }
}
