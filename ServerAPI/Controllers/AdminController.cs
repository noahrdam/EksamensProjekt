using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories.Interfaces;
using Core.Model;
using MongoDB.Bson;
using static System.Net.WebRequestMethods;
using MongoDB.Driver;

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
            return mRepo.GetAllApplication();
        }
    }
}