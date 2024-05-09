using Core.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Application> CreateApplicationAsync(Application application);
        Task<Application> GetApplicationByIdAsync(int id);
        Task UpdateApplicationAsync(Application application);
        Task DeleteApplicationAsync(int id);
    }
}