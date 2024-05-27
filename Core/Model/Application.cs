using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;


namespace Core.Model
{
    public class Application
    {

        public ObjectId Id { get; set; }
        public int ApplicationId { get; set; }

        public bool BeenBefore { get; set; }

        [Required(ErrorMessage = "Kommentar-felt skal angives")]
        public string Comment { get; set; }

        public string ConsentForm { get; set; }

        public ParentVolunteer ParentVolunteer { get; set; } = new ParentVolunteer();

        public Event FirstPrio { get; set; } = new Event();

        public Event SecondPrio { get; set; } = new Event();

        public string Status { get; set; } = "1.Prioritet"; 

        public bool IsPublished { get; set; } = false;

        public DateTime DateOfApplication { get; set; } = DateTime.Now;

        public Application()
        {
            DateOfApplication = DateTime.Now;
        }

    }
}
