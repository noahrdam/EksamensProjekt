using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/register")]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationController(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        [HttpPost]
        [Route("upload/{applicationId}")]
        public async Task<IActionResult> UploadFile(int applicationId, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var application = await _registrationRepository.GetApplicationByIdAsync(applicationId);
                if (application == null)
                {
                    return NotFound();
                }

                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    application.ConsentForm = Convert.ToBase64String(ms.ToArray());
                    await _registrationRepository.UpdateApplicationAsync(application);
                }

                return Ok(new { message = "File uploaded successfully" });
            }

            return BadRequest(new { message = "No file uploaded" });
        }
    }
}

