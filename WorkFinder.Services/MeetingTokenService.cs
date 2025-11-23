using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts;
using static System.Net.WebRequestMethods;

namespace WorkFinder.Services
{
    /// <summary>
    /// Service Implementation to get zoom token
    /// </summary>
    public class MeetingTokenService : IMeetingTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public MeetingTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Get zoom token
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetAccessTokenAsync()
        {
            string? accountId = _configuration["Zoom:AccountId"];
            string? clientId = _configuration["Zoom:ClientId"];
            string? clientSecret = _configuration["Zoom:ClientSecret"];

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);

            string url = $"https://zoom.us/oauth/token?grant_type=account_credentials&account_id={accountId}";

            var response = await _httpClient.PostAsync(url, null);
            var jsonString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching Zoom token: " + jsonString);

            var json = JsonSerializer.Deserialize<JsonElement>(jsonString);
            return json.GetProperty("access_token").GetString();
        }
    }
}
