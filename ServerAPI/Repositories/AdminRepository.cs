using MongoDB.Bson;
using MongoDB.Driver;
using Core.Model;
using ServerAPI.Repositories.Interfaces;


namespace ServerAPI.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private IMongoClient client;
        private IMongoCollection<Application> applicationcollection;
        private readonly IMongoCollection<Event> eventcollection;

        public AdminRepository()
        {

            var mongoUri = "mongodb+srv://eaa23fana:4321@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
            //var mongoUri = "mongodb://localhost:27017";
            client = new MongoClient(mongoUri);

            var database = client.GetDatabase("ChildClub");

            applicationcollection = database.GetCollection<Application>("Application");
            eventcollection = database.GetCollection<Event>("Event");

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

        public void UpdateFinalDate(int applicationId, int finalDatePriority)
        {
            var application = applicationcollection.Find(a => a.ApplicationId == applicationId).FirstOrDefault();
            if (application != null)
            {
                if (finalDatePriority == 1)
                {
                    application.FinalDate = application.FirstPrio;
                }
                else if (finalDatePriority == 2)
                {
                    application.FinalDate = application.SecondPrio;
                    applicationcollection.ReplaceOne(a => a.ApplicationId == applicationId, application);
                }
                
            }
        }


    }
}