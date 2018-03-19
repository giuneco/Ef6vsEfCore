using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model;

namespace EFCore.Models
{
    public class BookViewModel
    {
        public Library FatherLibrary { get; set; }
    }
}
