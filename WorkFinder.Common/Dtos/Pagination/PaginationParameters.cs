using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Common.Dtos.Pagination
{
    public class PaginationParameters<T>
    {
        public T? Filters { get; set; }
        public string? SearchValue { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageNo { get; set; } = 1;
    }
}
