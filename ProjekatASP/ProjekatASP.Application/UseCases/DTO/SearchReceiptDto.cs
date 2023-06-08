using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class SearchReceiptDto : PagedSearch
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int? UserId { get; set; }
    }
}
