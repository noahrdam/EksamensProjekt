using Core.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ServerAPI.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        List<Application> GetAllApplication();
        List<Application> GetApplicationsByDetails(string searchKeyword);
    }
    

}