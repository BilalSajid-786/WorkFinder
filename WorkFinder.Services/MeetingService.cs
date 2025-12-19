using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Meeting;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation for creating zoom meeting
    /// </summary>
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingTokenService _tokenService;
        private readonly HttpClient _http;

        public MeetingService(IMeetingTokenService tokenService)
        {
            _tokenService = tokenService;
            _http = new HttpClient();
        }

        /// <summary>
        /// Create zoom meeting
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="startTime"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public async Task<string> CreateMeetingAsync(string topic, DateTime startTime, int duration)
        {
            var token = await _tokenService.GetAccessTokenAsync();

            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var body = new
            {
                topic,
                type = 2,
                start_time = startTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                duration,
                settings = new
                {
                    join_before_host = true,
                    waiting_room = false
                }
            };

            var res = await _http.PostAsJsonAsync("https://api.zoom.us/v2/users/me/meetings", body);
            var json = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
                throw new Exception("Zoom API Error: " + json);

            using var doc = JsonDocument.Parse(json);

            string joinUrl = doc.RootElement.GetProperty("join_url").GetString();

            return $"Your meeting {topic} has been scheduled for date {startTime.ToString("yyyy-MM-ddTHH:mm:ss")}. Click below link to attend the meeting.\n {joinUrl}";
        }
    }
}
