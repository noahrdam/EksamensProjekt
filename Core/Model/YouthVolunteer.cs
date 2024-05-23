using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    [BsonDiscriminator("YouthVolunteer")]
    public class YouthVolunteer : Volunteer
    {
        
        public string ConsentForm { get; set; }
        [Required(ErrorMessage = "Forældres navn skal angives")]
        public string ParentName { get; set; }

        public DateTime DateOfConsent { get; set; } = DateTime.Now;

        public YouthVolunteer()
        {
            DateOfConsent = DateTime.Now;
        }
    }
}
