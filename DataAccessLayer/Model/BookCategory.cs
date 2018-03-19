using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Model
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
