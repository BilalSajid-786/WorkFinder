using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Pagination
{
    public class PaginationRequestDto
    {
        public string SearchValue { get; set; } = string.Empty;
        public string SortColumn { get; set; } = string.Empty;
        public string SortOrder { get; set; } = string.Empty;
        [Required]
        public int PageSize { get; set; } = 5;
        [Required]
        public int PageNo { get; set; } = 1;
        [Required]
        public Guid EmployerId { get; set; }
    }
}
