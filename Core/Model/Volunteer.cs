using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Navn skal angives")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kræwnummer skal angives")]
        public int CrewNumber { get; set; }
        [Required(ErrorMessage = "Mail skal angives")]
        public string Mail {  get; set; }
        [Required(ErrorMessage = "Telefonnummer skal angives")]
        public string Phone { get; set; }

    }
}
