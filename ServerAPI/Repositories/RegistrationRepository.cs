using Core.Model;
using MongoDB.Driver;
using MongoDB.Bson;
using ServerAPI.Repositories.Interfaces;

namespace ServerAPI.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private IMongoClient client;
        private IMongoCollection<Application> applicationcollection;
        private IMongoCollection<Parent> parentcollection;
        private IMongoCollection<Child> childrenCollection; // New collection for Children

        public RegistrationRepository()
        {
            var password = "4321";
            var mongoUri = $"mongodb+srv://eaa23fana:{password}@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
            //var mongoUri= "mongodb://localhost:27017";


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
            var ApplicationCollectionName = "Application";
            var ParentCollectionName = "Parent";
            var ChildrenCollectionName = "Children"; // Name for the children collection


            applicationcollection = client.GetDatabase(dbName).GetCollection<Application>(ApplicationCollectionName);

            parentcollection = client.GetDatabase(dbName).GetCollection<Parent>(ParentCollectionName);

            childrenCollection = client.GetDatabase(dbName).GetCollection<Child>(ChildrenCollectionName); // Initialize children collection


        }

        public Parent AddParent(Parent parent)
        {
            // Check if the parent already exists based on the unique identifier (e.g., CrewNumber)
            var existingParent = parentcollection.Find(p => p.CrewNumber == parent.CrewNumber).FirstOrDefault();
            if (existingParent == null)
            {
                // If the parent does not exist, determine the next ParentId
                var maxParentId = 0;
                if (parentcollection.CountDocuments(Builders<Parent>.Filter.Empty) > 0)
                {
                    maxParentId = parentcollection
                        .Find(Builders<Parent>.Filter.Empty)
                        .SortByDescending(p => p.ParentId)
                        .Limit(1)
                        .FirstOrDefault()
                        .ParentId;
                }
                parent.ParentId = maxParentId + 1;

                // Insert the new parent into the database
                parentcollection.InsertOne(parent);
                return parent; // Return the newly added parent with an ID
            }
            return existingParent; // Return the existing parent if found
        }

        public void RegisterApplication(Application application)
        {
            var parent = AddParent(application.Parent);

            var max = 0;
            if (applicationcollection.Count(Builders<Application>.Filter.Empty) > 0)
            {
                max = applicationcollection.Find(Builders<Application>.Filter.Empty).SortByDescending(r => r.ApplicationId).Limit(1).ToList()[0].ApplicationId;
            }
            application.ApplicationId = max + 1;

            applicationcollection.InsertOne(application);
        }

        public List<Event> GetEvents()
        {
            var dbName = "ChildClub";
            var collectionName = "Event";

            var eventCollection = client.GetDatabase(dbName)
               .GetCollection<Event>(collectionName);

            return eventCollection.Find(Builders<Event>.Filter.Empty).ToList();
        }

    }
}
