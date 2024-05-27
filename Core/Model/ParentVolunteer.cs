using MongoDB.Bson.Serialization.Attributes;


namespace Core.Model
{
    [BsonDiscriminator("ParentVolunteer")]
    public class ParentVolunteer : Volunteer
    {
        public bool Newsletter { get; set; }
        public List<Child> Children { get; set; } = new List<Child>();
    }
}
