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

        }

        public void AddParent(Parent parent)
        {
            var existingParent = parentcollection.Find(p => p.CrewNumber == parent.CrewNumber).FirstOrDefault();
            if (existingParent == null)
            {
                var maxParentId = 0;
                if (parentcollection.Count(Builders<Parent>.Filter.Empty) > 0)
                {
                    maxParentId = parentcollection
                        .Find(Builders<Parent>.Filter.Empty)
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
                application.Parent.ParentId = parent.ParentId;  // Link the application to the existing parent ID

                // Check if adding a new child would exceed the limit of 2 children per parent
                if (parent.Children.Count < 2)
                {
                    // Update parent with the new child only if they have fewer than 2 children
                    UpdateParent(parent, application.Parent.Children[0]);
                }
                else
                {
                    // Optionally, handle the case where a parent already has 2 children and is attempting to add more
                    throw new Exception("A parent can only apply for two children.");
                }
            }
            else
            {
                // If it's a new parent, add them
                AddParent(application.Parent);
            }

            // Assign a new application ID
            var max = 0;
            if (applicationcollection.Count(Builders<Application>.Filter.Empty) > 0)
            {
                max = applicationcollection.Find(Builders<Application>.Filter.Empty).SortByDescending(r => r.ApplicationId).Limit(1).ToList()[0].ApplicationId;
            }
            application.ApplicationId = max + 1;

            // Insert the application into the collection
            applicationcollection.InsertOne(application);
        }


        public void UpdateParent(Parent existingParent, Child newChild)
        {
            if (!existingParent.Children.Any(c => c.Name == newChild.Name))
            {
                existingParent.Children.Add(newChild);
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
