using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Application
    {

        public ObjectId Id { get; set; }
        public int ApplicationId { get; set; }

        public bool BeenBefore { get; set; }

        public string Comment { get; set; }

        public string ConsentForm { get; set; }

        public ParentVolunteer ParentVolunteer { get; set; } = new ParentVolunteer();

        public Event FirstPrio { get; set; } = new Event();

        public Event SecondPrio { get; set; } = new Event();

        public string Status { get; set; } = "Under behandling"; 

        public DateTime DateOfApplication { get; set; } = DateTime.Now;

        public Application()
        {
            DateOfApplication = DateTime.Now;
        }

    }
}
