using MongoDB.Bson;
using MongoDB.Driver;
using Core.Model;
using ServerAPI.Repositories.Interfaces;
using System.Collections.Generic;

namespace ServerAPI.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IMongoClient client;
        private readonly IMongoCollection<Application> applicationcollection;
        private readonly IMongoCollection<Event> eventcollection;
        private readonly IMongoCollection<Volunteer> volunteercollection;

        public AdminRepository()
        {
            var mongoUri = "mongodb+srv://eaa23fana:4321@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
            client = new MongoClient(mongoUri);

            var database = client.GetDatabase("ChildClub");

            applicationcollection = database.GetCollection<Application>("Application");
            eventcollection = database.GetCollection<Event>("Event");
            volunteercollection = database.GetCollection<Volunteer>("Volunteer"); // Initialiser volunteercollection
        }

        public List<Application> GetAllApplication()
        {
            return applicationcollection.Find(Builders<Application>.Filter.Empty).ToList();
        }

        public List<Application> GetFilteredApplications(FilterDefinition<Application> filter)
        {
            return applicationcollection.Find(filter).ToList();
        }

        public List<Event> GetAllEvents()
        {
            return eventcollection.Find(Builders<Event>.Filter.Empty).ToList();
        }

        public async Task UpdateApplication(Application application)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.ApplicationId, application.ApplicationId);
            await applicationcollection.ReplaceOneAsync(filter, application);
        }

        public void UpdateFinalDate(Application application)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.ApplicationId, application.ApplicationId);
            var update = Builders<Application>.Update.Set(a => a.Status, application.Status);
            applicationcollection.UpdateOneAsync(filter, update);
        }

        public List<YouthVolunteer> GetAllYouthVolunteers()
        {
            var filter = Builders<Volunteer>.Filter.Eq("_t", "YouthVolunteer");
            var volunteers = volunteercollection.Find(filter).ToList();
            return volunteers.ConvertAll(v => (YouthVolunteer)v);
        }

        public void DeleteApplication(int applicationId)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.ApplicationId, applicationId);
            applicationcollection.DeleteOne(filter);
        }

    }
}
