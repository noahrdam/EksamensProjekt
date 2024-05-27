using MongoDB.Bson;


namespace Core.Model
{

    public class Admin
    {
        
        public ObjectId Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


}

