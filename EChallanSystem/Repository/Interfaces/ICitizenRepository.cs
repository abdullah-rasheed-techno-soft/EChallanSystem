using EChallanSystem.Models;

namespace EChallanSystem.Repository.Interfaces
{
    public interface ICitizenRepository
    {
        Task<List<Citizen>> GetCitizens();
        Task<Citizen> GetCitizen(int id);
        Task<List<Citizen>> AddCitizen(Citizen newCitizen);
    }
}
