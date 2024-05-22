// AdminController.cs
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories.Interfaces;
using Core.Model;
using MongoDB.Bson;
using static System.Net.WebRequestMethods;
using MongoDB.Driver;
using static System.Net.Mime.MediaTypeNames;
using Application = Core.Model.Application;
using ServerAPI.Repositories;

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
        [Route("getallevents")]
        public List<Event> GetAllEvents()
        {
            return mRepo.GetAllEvents();
        }

        [HttpGet]
        [Route("getallyouthvolunteers")]
        public IActionResult GetAllYouthVolunteers()
        {
            var volunteers = mRepo.GetAllYouthVolunteers();
            return Ok(volunteers);
        }

        [HttpDelete("delete/{id}")]
        public void DeleteApplication(int id)
        {
            mRepo.DeleteApplication(id);
        }



        [HttpPut]
        [Route("updatestatus")]
        public void UpdateStatus(Application application)
        {
            mRepo.UpdateStatus(application);
            
        }

		[HttpGet("getfilteredbyweek")]
		public List<Application> GetFilteredApplicationsByWeek([FromQuery]int week)
		{
			var applications = mRepo.GetFilteredApplicationsByWeek(week);
            return applications;
		}

        [HttpGet("getfilteredbyperiod")]
        public List<Application> GetFilteredApplicationsByPeriod([FromQuery] int week, [FromQuery] string from, [FromQuery] string to)
        {
            var applications = mRepo.GetFilteredApplicationsByPeriod(week, from, to);
            return applications;
        }

        [HttpPut]
        [Route("publishall")]
        public void PublishAllApplications(List<Application> applications)
        {
            mRepo.PublishAllApplications(applications);
        }
    }
}
