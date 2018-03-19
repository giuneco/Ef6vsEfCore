using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Model
{
    public class Book
    {
        public Book()
        {
            
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }
        public virtual IList<BookCategory> BookCategories { get; set; }
    }
}
