using EChallanSystem.Models;

namespace EChallanSystem.Repository.Interfaces
{
    public interface IChallanEmailRepository
    {
        Task<List<ChallanEmail>> GetChallanEmails();
      
        Task<List<ChallanEmail>> SendChallanEmail(ChallanEmail newChallanEmail);
    }
}
