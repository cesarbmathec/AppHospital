using AppHospital.Models;

namespace AppHospital.Interfaces
{
    public interface IServiceAPI
    {
        Task<List<Patient>?> GetPatientList();
        Task<Patient?> GetPatient(int? id);
        Task<bool> CreatePatient(Patient patient);
        Task<bool> UpdatePatient(Patient patient);
        Task<bool> DeletePatient(int id);

        Task<Token?> GetToken(Credential credential);
        void Autentication(Token token);
    }
}