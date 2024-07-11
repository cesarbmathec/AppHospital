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

        // Constructor
        public ServiceAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseURL);

        }

        public Task<Patient?> GetPatient(int? id)
        {
            throw new NotImplementedException();
        }
        public Task<List<Patient>?> GetPatientList()
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

        public Task<Token> GetToken(Credential credential)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Autentication()
        {
            throw new NotImplementedException();
        }
    }

}
