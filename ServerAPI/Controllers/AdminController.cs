// AdminController.cs
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories.Interfaces;
using Core.Model;
using ServerAPI.EmailCommunication;


namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/aps")]
    public class AdminController : ControllerBase
    {
        private IAdminRepository mRepo;
        private IEmail _emailService;

        public AdminController(IAdminRepository repo, IEmail emailService)
        {
            mRepo = repo;
            _emailService = emailService;
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
        public List<Application> GetFilteredApplicationsByWeek([FromQuery] int week)
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

        [HttpPost]
        [Route("sendmanyemails")]
        public void SendManylEmails(List<Application> applications)
        {
            foreach (var application in applications)
            {
                if (!application.IsPublished)
                {
                    var subject = "Bekræftelse på din ansøgning";
                    string body;
                    string facebookGroupUrl = "";

                    // Determine the Facebook group URL based on the priority
                    if (application.Status == "1.Prioritet")
                    {
                        facebookGroupUrl = $"https://www.facebook.com/groups/uge{application.FirstPrio.Week}cirku";
                        body = $"♥ Kære {application.ParentVolunteer.Name}♥\n\n" +
                               $"Dette er mailen som bekræfter jeres plads i børneklubben for\n" +
                               $"{application.FirstPrio.From} til {application.FirstPrio.To} i uge {application.FirstPrio.Week} i {application.FirstPrio.Location} 2024 (Hurra)\n\n" +
                               $"Jeg håber I får en kanon-god halv eller hel uge, på Cirkus Summarum til sommer. " +
                               $"Jeg håber I har nogle børn der glæder sig rigtigt meget! 🎪\n\n" +
                               $"I børneklubben vil I aflevere jeres børn hos de kære frivillige, som vil tage godt hånd om jeres unge 🌟.\n\n" +
                               $"Der er lavet en facebookgruppe, så I kan starte snakken og se hvem jeres børn skal bruge tiden sammen med, " +
                               $"mens I er ude på cirkuspladsen og være seje! [UGE {application.FirstPrio.Week} CIRKUS SUMMARUMS BØRNEKLUB | Facebook]({facebookGroupUrl})\n\n" +
                               $"(Husk at melde jer ind i den, da videre kommunikationen kommer derigennem)\n\n" +
                               $"## Husk, hvis jeres planer pludselig ændrer sig, så tøv ikke med at fortælle mig det. Så er folk på venteliste nemlig glade! 🌈 ##\n\n" +
                               $"Forventning: At I allerede har tilmeldt jer arrangementet og fået plads - da der er venteliste på flere af ugerne allerede.\n\n" +
                               $"Hvis ikke, så vend gerne hurtigt tilbage.\n\n" +
                               $"Venlig hilsen,\nBørneklubben";
                    }
                    else if (application.Status == "2.Prioritet")
                    {
                        facebookGroupUrl = $"https://www.facebook.com/groups/uge{application.SecondPrio.Week}cirku";
                        body = $"♥ Kære {application.ParentVolunteer.Name}♥\n\n" +
                               $"Dette er mailen som bekræfter jeres plads i børneklubben for\n" +
                               $"{application.SecondPrio.From} til {application.SecondPrio.To} i uge {application.SecondPrio.Week} i {application.SecondPrio.Location} 2024 (Hurra)\n\n" +
                               $"Jeg håber I får en kanon-god halv eller hel uge, på Cirkus Summarum til sommer. " +
                               $"Jeg håber I har nogle børn der glæder sig rigtigt meget! 🎪\n\n" +
                               $"I børneklubben vil I aflevere jeres børn hos de kære frivillige, som vil tage godt hånd om jeres unge 🌟.\n\n" +
                               $"Der er lavet en facebookgruppe, så I kan starte snakken og se hvem jeres børn skal bruge tiden sammen med, " +
                               $"mens I er ude på cirkuspladsen og være seje! [UGE {application.SecondPrio.Week} CIRKUS SUMMARUMS BØRNEKLUB | Facebook]({facebookGroupUrl})\n\n" +
                               $"(Husk at melde jer ind i den, da videre kommunikationen kommer derigennem)\n\n" +
                               $"## Husk, hvis jeres planer pludselig ændrer sig, så tøv ikke med at fortælle mig det. Så er folk på venteliste nemlig glade! 🌈 ##\n\n" +
                               $"Forventning: At I allerede har tilmeldt jer arrangementet og fået plads - da der er venteliste på flere af ugerne allerede.\n\n" +
                               $"Hvis ikke, så vend gerne hurtigt tilbage.\n\n" +
                               $"Venlig hilsen,\nBørneklubben";
                    }
                    else
                    {
           
                        body = $"♥ Kære {application.ParentVolunteer.Name}♥\n\n" + 
                               $"Vi har modtaget jeres ansøgning til børneklubben for\n" +
                               $"{application.FirstPrio.From} til {application.FirstPrio.To} i uge {application.FirstPrio.Week} i {application.FirstPrio.Location}\n\n" +
                               $"Desværre er alle pladser i jeres første- og andenprioritet fyldt op på nuværende tidspunkt, og I er derfor blevet placeret på ventelisten.\n\n" +
                               $"Vi håber, at I stadig ser frem til en kanon-god halv eller hel uge, på Cirkus Summarum til sommer. " +
                               $"Hvis der bliver en ledig plads, vil vi straks kontakte jer og bekræfte jeres deltagelse.\n\n" +
                               $"Venlig hilsen,\nBørneklubben";
                    }

                    _emailService.SendEmail(application.ParentVolunteer.Mail, subject, body);
                }
            }
        }

        [HttpPost]
        [Route("sendemail")]
        public void SendEmail(Core.Model.Email email)
        {
            _emailService.SendEmail(email.To, email.Subject, email.Body);
        }

    }
}
