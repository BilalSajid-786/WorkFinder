using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts
{
    /// <summary>
    /// Zoom Service Contract to create meeting
    /// </summary>
    public interface IMeetingService
    {
        Task<string> CreateMeetingAsync(string topic, DateTime startTime, int duration);
    }
}
