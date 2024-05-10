using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Model
{
    public class Application
    {
        public ObjectId Id { get; set; }

        public int ApplicationId { get; set; }

        public bool BeenBefore { get; set; }

        public string Comment { get; set; }

        public string ConsentForm { get; set; }

        public Parent Parent { get; set; }

        public Child Child { get; set; }

        public Event Event { get; set; }

    }
}