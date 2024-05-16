using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(ParentVolunteer), typeof(YouthVolunteer))]
    public class Volunteer
    {
        public ObjectId Id { get; set; }
        public int VolunteerId { get; set; }
        public string Name { get; set; }

        public int CrewNumber { get; set; }

        public string Mail {  get; set; }

        public string Phone { get; set; }

    }
}
