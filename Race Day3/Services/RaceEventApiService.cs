using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;



namespace Race_Day3.Services
{

  
        public class RaceEventApiService
        {
            private readonly HttpClient _httpClient;

            public RaceEventApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // READ: Get all events
            public async Task<List<Event>> GetAllEventsAsync()
            {
                var events = await _httpClient.GetFromJsonAsync<List<Event>>("api/events");
                return events ?? new List<Event>();
            }

            // READ: Get one event by ID
            public async Task<Event?> GetEventByIdAsync(int id)
            {
                var response = await _httpClient.GetAsync($"api/events/{id}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Event>();
            }

            // CREATE: Add event
            public async Task<bool> CreateEventAsync(Event newEvent)
            {
                var response = await _httpClient.PostAsJsonAsync("api/admin/events", newEvent);
                return response.IsSuccessStatusCode;
            }

            // DELETE: Remove event
            public async Task<bool> DeleteEventAsync(int id)
            {
                var response = await _httpClient.DeleteAsync($"api/admin/events/{id}");
                return response.IsSuccessStatusCode;
            }
        }
    }

