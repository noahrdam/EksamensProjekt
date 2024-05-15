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
        [Route("getbyweek")]
        public IActionResult GetApplicationsByWeek([FromQuery] int week, [FromQuery] string from = null, [FromQuery] string to = null)
        {
            FilterDefinition<Application> filter = Builders<Application>.Filter.Eq("FirstPrio.Week", week);

            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
            {
                filter &= Builders<Application>.Filter.Eq("FirstPrio.From", from) &
                          Builders<Application>.Filter.Eq("FirstPrio.To", to);
            }

            var applications = mRepo.GetFilteredApplications(filter);
            return Ok(applications);
        }

        [HttpGet]
        [Route("getallevents")]
        public List<Event> GetAllEvents()
        {
            return mRepo.GetAllEvents();
        }

        [HttpPost]
        [Route("updateapplication")]
        public async Task<IActionResult> UpdateApplication([FromBody] Application application)
        {
            if (application == null)
            {
                return BadRequest();
            }

            await mRepo.UpdateApplication(application);
            return Ok();
        }

        [HttpGet]
        [Route("getallyouthvolunteers")]
        public IActionResult GetAllYouthVolunteers()
        {
                var volunteers = mRepo.GetAllYouthVolunteers();
                return Ok(volunteers);
        }

        [HttpPut]
        [Route("updatefinaldate")]
        public void UpdateFinalDate(Application application)
        {
            mRepo.UpdateFinalDate(application);
        }

    }
}