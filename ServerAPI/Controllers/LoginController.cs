    using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories.Interfaces;
using Core.Model;

namespace ServerAPI.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _repo;

        public LoginController(ILoginRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("verify")]
        public bool VerifyLogin([FromQuery] string username, [FromQuery] string password)
        {
            return _repo.VerifyLogin(username, password);
        }

        [HttpGet]
        [Route("getadmin")]
        public Admin? GetAdmin([FromQuery] string username)
        {
            return _repo.GetAdmin(username);
        }
    }
}
