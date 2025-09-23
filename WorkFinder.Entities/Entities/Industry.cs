using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Industry
    {
        public int IndustryId { get; set; }
        public string IndustryName { get; set; } = string.Empty;
        public IEnumerable<Job>? Jobs { get; set; }
    }
}
