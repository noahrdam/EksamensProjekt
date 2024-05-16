using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    [BsonDiscriminator("YouthVolunteer")]
    public class YouthVolunteer : Volunteer
    {
        public string ConsentForm { get; set; }

        public string ParentName { get; set; }

        public DateTime DateOfConsent { get; set; }
    }
}
