using Race_Day3.Models;
using System.Net;
using System.Net.Http.Json;

namespace Race_Day3.Services
{
    public class RaceEventApiService
    {
        private readonly HttpClient _httpClient;

        public RaceEventApiService(HttpClient httpClient) => _httpClient = httpClient;

        // EVENTS
        public async Task<List<Event>> GetAllEventsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Event>>("api/events") ?? new();
            
        public async Task<Event?> GetEventByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Event>($"api/events/{id}");

        public async Task<bool> CreateEventAsync(Event ev) =>
            (await _httpClient.PostAsJsonAsync("api/admin/events", ev)).IsSuccessStatusCode;

        // ENROLMENTS
        public async Task<List<Enrolment>> GetMyEnrolmentsAsync(int userId) =>
            await _httpClient.GetFromJsonAsync<List<Enrolment>>($"api/enrolments/user/{userId}") ?? new();

        public async Task<bool> EnrolAsync(int userId, int eventId)
        {
            var enrolment = new { UserId = userId, EventId = eventId };
            var response = await _httpClient.PostAsJsonAsync("api/enrolments", enrolment);
            return response.IsSuccessStatusCode;
        }

        // FEEDBACK
        public async Task<List<Feedback>> GetFeedbackForEventAsync(int eventId) =>
            await _httpClient.GetFromJsonAsync<List<Feedback>>($"api/feedback/event/{eventId}") ?? new();

        public async Task<bool> SubmitFeedbackAsync(int eventId, int userId, string comment, int rating)
        {
            var request = new { UserId = userId, Comment = comment, Rating = rating };
            var response = await _httpClient.PostAsJsonAsync($"api/feedback/event/{eventId}", request);
            return response.IsSuccessStatusCode;
        }
    }
}
