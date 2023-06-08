using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.Queries
{
    public class PageResponse<T> 
        where T : class
    {
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int PagesCount => (int)Math.Ceiling((float)Count / PerPage);
        public IEnumerable<T> Data { get; set; }
    }
}
