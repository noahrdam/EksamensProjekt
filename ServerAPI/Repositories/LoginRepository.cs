using MongoDB.Bson;
using MongoDB.Driver;
using Core.Model;
using ServerAPI.Repositories.Interfaces;

namespace ServerAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IMongoCollection<Admin> _adminCollection;

        public LoginRepository()
        {
            //var mongoUri = "mongodb+srv://eaa23fana:4321@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
            var mongoUri = "mongodb://localhost:27017";
            var client = new MongoClient(mongoUri);
            var database = client.GetDatabase("ChildClub");
            _adminCollection = database.GetCollection<Admin>("Admin");
        }

        public Admin? GetAdmin(string username)
        {
            var filter = Builders<Admin>.Filter.Eq("Username", username);

            return _adminCollection.Find(filter).SingleOrDefault();
        }

        public bool VerifyLogin(string username, string password)
        {
            var filter = Builders<Admin>.Filter.Eq("Username", username) & Builders<Admin>.Filter.Eq("Password", password);
            var user = _adminCollection.Find(filter).SingleOrDefault();

            return user != null;
        }
    }
}
