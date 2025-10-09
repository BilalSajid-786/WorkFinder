using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Pagination
    {
        public string SearchValue { get; set; } = string.Empty;
        public string SortColumn { get; set; } = string.Empty;
        public string SortOrder { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        //public Guid EmployerId { get; set; }
    }
}
