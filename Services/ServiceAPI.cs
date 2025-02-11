using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using AppHospital.Models;
using AppHospital.Interfaces;

namespace AppHospital.Services
{
    public class ServiceAPI : IServiceAPI
    {
        private readonly HttpClient _httpClient;
        private string _baseURL = "http://127.0.0.1:8000/";
        private bool isAuthenticate { set; get; } = false;
        private string? _token;

        // Constructor
        public ServiceAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseURL);
        }

        public async Task<List<Patient>?> GetPatientList()
        {
            if (isAuthenticate)
            {
                try
                {
                    var response = await _httpClient.GetAsync("/historias_medicas/paciente/");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<List<Patient>>(jsonResponse);
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Algo salio mal");
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"Error: {e}");
                    return null;
                }
            }
            return null;
        }

        public async Task<Patient?> GetPatient(int? id)
        {
            if (isAuthenticate)
            {
                if (id != null)
                {
                    var response = await _httpClient.GetAsync($"historias_medicas/paciente/{id}/");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<Patient>(jsonResponse);
                    }
                }
            }

            return null;
        }

        public Task<bool> CreatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePatient(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public async Task<Token?> GetToken(Credential credential)
        {
            Token? result = new Token();
            var content = new StringContent(JsonSerializer.Serialize(credential), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("api-token-auth/", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<Token>(jsonResponse);
                }

                return result;
            }
            catch (System.Exception e)
            {

                Console.WriteLine($"Error: {e}");
                return null;
            }
        }

        public async Task<bool> Authenticate(Credential credential)
        {
            Token? token = await GetToken(credential);
            if (token != null && token.token != null)
            {
                _token = token.token;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _token);

                // Verify if the authentication is successful
                var response = await _httpClient.GetAsync("historias_medicas/paciente/");
                isAuthenticate = response.IsSuccessStatusCode;
                return isAuthenticate;
            }
            return false;
        }
    }

}
