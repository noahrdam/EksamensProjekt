using Core.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ServerAPI.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        List<Application> GetAllApplication();
        List<Application> GetFilteredApplications(FilterDefinition<Application> filter);
        List<Event> GetAllEvents();

        Task UpdateApplication(Application application);

        void UpdateFinalDate(Application application);

    }

}