
using BookStore.Models;
using System.Collections.Generic;

namespace BookStore.ViewModels
{
    public class HomeViewModel
    {
        public List<BookCardViewModel> Books { get; set; }

        public List<Author> Authors { get; set; }
    }
}
