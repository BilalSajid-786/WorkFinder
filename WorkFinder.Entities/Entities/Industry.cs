using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Industry
    {
        public Guid IndustryId { get; set; }
        public string IndustryName { get; set; } = string.Empty;
    }
}
