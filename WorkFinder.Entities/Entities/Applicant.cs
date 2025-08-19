using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class Applicant : User
    {
        public Guid ApplicantId { get; set; }
    }
}
