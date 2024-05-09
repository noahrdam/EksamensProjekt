using Core.Interfaces;
using Core.Model;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private IMongoClient client;
        private IMongoCollection<Application> collection;

        public RegistrationRepository()
        {

            var mongoUri = "mongodb+srv://eaa23fana:4321@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
            //var mongoUri = "mongodb://localhost:27017";


            try
            {
                client = new MongoClient(mongoUri);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting to your " +
                    "Atlas cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
                Console.WriteLine(e);
                Console.WriteLine();
                return;
            }

            // Provide the name of the database and collection you want to use.
            // If they don't already exist, the driver and Atlas will create them
            // automatically when you first write data.
            var dbName = "ChildClub";
            var collectionName = "Application";

            collection = client.GetDatabase(dbName)
               .GetCollection<Application>(collectionName);

        }

        public async Task<Application> CreateApplicationAsync(Application application)
        {
            await collection.InsertOneAsync(application);
            return application;
        }

        public async Task<Application> GetApplicationByIdAsync(int id)
        {
            return await collection.Find(x => x.ApplicationId == id).FirstOrDefaultAsync();
        }

        public async Task UpdateApplicationAsync(Application application)
        {
            await collection.ReplaceOneAsync(x => x.ApplicationId == application.ApplicationId, application);
        }

        public async Task DeleteApplicationAsync(int id)
        {
            await collection.DeleteOneAsync(x => x.ApplicationId == id);
        }
    }
}
