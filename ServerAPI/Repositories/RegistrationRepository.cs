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
        private IMongoCollection<Volunteer> volunteercollection;

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
            var VolunteerCollectionName = "Volunteer";
            var ChildrenCollectionName = "Children"; // Name for the children collection


            applicationcollection = client.GetDatabase(dbName).GetCollection<Application>(ApplicationCollectionName);

            volunteercollection = client.GetDatabase(dbName).GetCollection<Volunteer>(VolunteerCollectionName);

        }

        public void AddVolunteer(Volunteer volunteer)
        {
            var existingVolunteer = volunteercollection.Find(v => v.CrewNumber == volunteer.CrewNumber).FirstOrDefault();
            if (existingVolunteer == null)
            {
                var maxParentId = 0;
                if (volunteercollection.Count(Builders<Volunteer>.Filter.Empty) > 0)
                {
                    maxParentId = volunteercollection
                        .Find(Builders<Volunteer>.Filter.Empty)
                        .SortByDescending(p => p.ParentId)
                        .Limit(1)
                        .ToList()[0]
                        .ParentId;
                }
                volunteer.ParentId = maxParentId + 1;

                volunteercollection.InsertOne(volunteer);
            }
        }

        public void RegisterApplication(Application application)
        {

            var volunteer = volunteercollection.Find(p => p.CrewNumber == application.Volunteer.CrewNumber).FirstOrDefault();

            if (volunteer != null)
            {
                if (volunteer.Children.Count + application.Volunteer.Children.Count > 2)
                {
                    return;
                }
                else
                {
                    application.Volunteer.ParentId = volunteer.ParentId;
                    UpdateVolunteer(volunteer);
                }
            }
            else
            {
                AddVolunteer(application.Volunteer);
            }

            var max = 0;
            if (applicationcollection.Count(Builders<Application>.Filter.Empty) > 0)
            {
                max = applicationcollection.Find(Builders<Application>.Filter.Empty).SortByDescending(r => r.ApplicationId).Limit(1).ToList()[0].ApplicationId;
            }
            application.ApplicationId = max + 1;

            applicationcollection.InsertOne(application);
        }

        public void UpdateVolunteer(Volunteer volunteer)
        {
            var existingVolunteer = volunteercollection.Find(p => p.CrewNumber == volunteer.CrewNumber).FirstOrDefault();
            if (existingVolunteer != null && existingVolunteer.Children.Count < 2)
            {
                // Update existing parent's children list if less than 2 children
                foreach (var newChild in volunteer.Children)
                {
                    if (!existingVolunteer.Children.Any(c => c.Name == newChild.Name))
                    {
                        existingVolunteer.Children.Add(newChild);
                    }
                }
                volunteercollection.ReplaceOne(p => p.CrewNumber == existingVolunteer.CrewNumber, existingVolunteer);
            }
        }

        public void AddYouthVolunteer(YouthVolunteer youthVolunteer)
        {
            var existingVolunteer = volunteercollection.Find(v => v.CrewNumber == youthVolunteer.CrewNumber).FirstOrDefault();
            if (existingVolunteer == null)
            {
                var maxParentId = 0;
                if (volunteercollection.Count(Builders<Volunteer>.Filter.Empty) > 0)
                {
                    maxParentId = volunteercollection
                        .Find(Builders<Volunteer>.Filter.Empty)
                        .SortByDescending(p => p.ParentId)
                        .Limit(1)
                        .ToList()[0]
                        .ParentId;
                }
                youthVolunteer.ParentId = maxParentId + 1;

                volunteercollection.InsertOne(youthVolunteer);
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
