﻿using EChallanSystem.Models;

namespace EChallanSystem.Repository.Interfaces
{
    public interface IChallanRepository
    {
        Task<List<Challan>> GetChallans();
        Task<Challan> GetChallan(int id);
        Task<List<Challan>> CreateChallan(Challan newChallan);

        bool PayChallan(int id);
        bool ChallanExists(int id);
        bool Save();
    }
}
