using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Meeting
{
    public class MeetingRequestDto
    {
        public string Topic { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
    }
}
