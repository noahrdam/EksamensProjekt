using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories.Interfaces;
using Core.Model;
using MongoDB.Bson;
using static System.Net.WebRequestMethods;
using MongoDB.Driver;
using static System.Net.Mime.MediaTypeNames;
using Application = Core.Model.Application;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/aps")]
    public class AdminController : ControllerBase
    {
        private IAdminRepository mRepo;

        public AdminController(IAdminRepository repo)
        {
            mRepo = repo;
        }

        [HttpGet]
        [Route("getall")]
        public List<Application> GetAllApplication()
        {

            var applications = mRepo.GetAllApplication();
            return applications;
        }

        [HttpGet]
        [Route("search")]
        public List<Application> GetApplicationsByDetails([FromQuery] string searchKeyword)
        {
            return mRepo.GetApplicationsByDetails(searchKeyword);
        }
    }
}

