﻿// AdminController.cs
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
        public async Task<IActionResult> UpdateStatus([FromBody] Application application)
        {
            if (application == null)
            {
                return BadRequest();
            }

            await mRepo.UpdateStatus(application);
            return Ok();
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
	}
}
