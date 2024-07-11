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
        private string? _token;
        private bool isAuthenticate = false;

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
                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"Error: {e}");
                }
            }

            return null;
        }

        public Task<Patient?> GetPatient(int? id)
        {
            throw new NotImplementedException();
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

        public async void Autentication(Token token)
        {
            try
            {
                // Autenticamos
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _token);

                // Realizamos un end endpoint para verificar si hubo autenticación
                var response = await _httpClient.GetAsync("historias_medicas/paciente/");

                if (response.IsSuccessStatusCode)
                {
                    isAuthenticate = true;
                }
                else
                {
                    isAuthenticate = false;
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error: {e}");
            }
        }
    }

}
