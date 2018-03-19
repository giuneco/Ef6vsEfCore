using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Model
{
    public class Library
    {
        public Library()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual IList<Book> Books { get; set; }
    }
}
