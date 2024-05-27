using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories.Interfaces;
using Core.Model;

namespace ServerAPI.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        IRegistrationRepository mRepo;

        public RegistrationController(IRegistrationRepository repo)
        {
            mRepo = repo;
        }

        [HttpPost]
        [Route("register")]
        public void RegisterApplication(Application application)
        {
            mRepo.RegisterApplication(application);
        }

        [HttpPost]
        [Route("registerYouthVolunteer")]
        public void RegisterYouthVolunteer(YouthVolunteer youthVolunteer)
        {
            mRepo.AddYouthVolunteer(youthVolunteer);
        }

        [HttpGet]
        [Route("getallevents")]
        public List<Event> GetEvents()
        {
            return mRepo.GetEvents();
        }
    }
}
