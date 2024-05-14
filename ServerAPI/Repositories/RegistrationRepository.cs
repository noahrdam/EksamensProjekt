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
        private IMongoCollection<Volunteer> parentcollection;

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

            
            var dbName = "ChildClub";
            var ApplicationCollectionName = "Application";
            var ParentCollectionName = "Volunteer";


            applicationcollection = client.GetDatabase(dbName).GetCollection<Application>(ApplicationCollectionName);

            parentcollection = client.GetDatabase(dbName).GetCollection<Volunteer>(ParentCollectionName);

        }

        public void AddParent(Volunteer parent)
        {
            var existingParent = parentcollection.Find(p => p.CrewNumber == parent.CrewNumber).FirstOrDefault();
            if (existingParent == null)
            {
                var maxParentId = 0;
                if (parentcollection.Count(Builders<Volunteer>.Filter.Empty) > 0)
                {
                    maxParentId = parentcollection
                        .Find(Builders<Volunteer>.Filter.Empty)
                        .SortByDescending(p => p.ParentId)
                        .Limit(1)
                        .ToList()[0]
                        .ParentId;
                }
                parent.ParentId = maxParentId + 1;

                parentcollection.InsertOne(parent);
            }
        }

        public void RegisterApplication(Application application)
        {

            var parent = parentcollection.Find(p => p.CrewNumber == application.Parent.CrewNumber).FirstOrDefault();

            if (parent != null)
            {
                if (parent.Children.Count + application.Parent.Children.Count > 2)
                {
                    return;
                }
                else
                {
                    application.Parent.ParentId = parent.ParentId;
                    UpdateParent(parent);
                }
            }
            else
            {
                AddParent(application.Parent);
            }


            var max = 0;
            if (applicationcollection.Count(Builders<Application>.Filter.Empty) > 0)
            {
                max = applicationcollection.Find(Builders<Application>.Filter.Empty).SortByDescending(r => r.ApplicationId).Limit(1).ToList()[0].ApplicationId;
            }
            application.ApplicationId = max + 1;

            applicationcollection.InsertOne(application);
        }

        public void UpdateParent(Volunteer parent)
        {
            var existingParent = parentcollection.Find(p => p.CrewNumber == parent.CrewNumber).FirstOrDefault();
            if (existingParent != null && existingParent.Children.Count < 2)
            {
                // Update existing parent's children list if less than 2 children
                foreach (var newChild in parent.Children)
                {
                    if (!existingParent.Children.Any(c => c.Name == newChild.Name))
                    {
                        existingParent.Children.Add(newChild);
                    }
                }
                parentcollection.ReplaceOne(p => p.CrewNumber == existingParent.CrewNumber, existingParent);
            }
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
