using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    [BsonDiscriminator("ParentVolunteer")]
    public class ParentVolunteer : Volunteer
    {
        public List<Child> Children { get; set; } = new List<Child>();
    }
}
