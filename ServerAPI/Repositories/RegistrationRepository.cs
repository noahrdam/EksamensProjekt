using Core.Model;
using MongoDB.Driver;
using MongoDB.Bson;
using ServerAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerAPI.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private IMongoClient client;
        private IMongoCollection<Application> applicationCollection;
        private IMongoCollection<Volunteer> volunteerCollection;

        public RegistrationRepository()
        {
            //var mongoUri = "mongodb+srv://eaa23fana:4321@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
            var mongoUri = "mongodb://localhost:27017";

            try
            {
                client = new MongoClient(mongoUri);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting to your " +
                    "MongoDB cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
                Console.WriteLine(e);
                Console.WriteLine();
                return;
            }

            var dbName = "ChildClub";
            var applicationCollectionName = "Application";
            var volunteerCollectionName = "Volunteer";

            applicationCollection = client.GetDatabase(dbName).GetCollection<Application>(applicationCollectionName);
            volunteerCollection = client.GetDatabase(dbName).GetCollection<Volunteer>(volunteerCollectionName);
        }

        public void AddParentVolunteer(ParentVolunteer parentVolunteer)
        {
            var filter = Builders<Volunteer>.Filter.Eq(v => v.CrewNumber, parentVolunteer.CrewNumber);
            var existingParent = volunteerCollection.Find(filter).FirstOrDefault();
            if (existingParent == null)
            {
                var maxVolunteerId = 0;
                if (volunteerCollection.Count(Builders<Volunteer>.Filter.Empty) > 0)
                {
                    maxVolunteerId = volunteerCollection
                        .Find(Builders<Volunteer>.Filter.Empty)
                        .SortByDescending(p => p.VolunteerId)
                        .Limit(1)
                        .ToList()[0]
                        .VolunteerId;
                }
                parentVolunteer.VolunteerId = maxVolunteerId + 1;

                volunteerCollection.InsertOne(parentVolunteer);
            }
        }

        public void RegisterApplication(Application application)
        {
            var filter = Builders<Volunteer>.Filter.Eq(v => v.CrewNumber, application.ParentVolunteer.CrewNumber);
            var volunteer = volunteerCollection.Find(filter).FirstOrDefault();

            if (volunteer is ParentVolunteer parentVolunteer)
            {
                application.ParentVolunteer.VolunteerId = parentVolunteer.VolunteerId;
                
                // Check if adding a new child would exceed the limit of 2 children per parent
                if (parentVolunteer.Children.Count + application.ParentVolunteer.Children.Count > 2)
                {
                    throw new Exception("A parent can only apply for two children.");
                }

                // Update parent with the new child only if they have fewer than 2 children
                foreach (var child in application.ParentVolunteer.Children)
                {
                    UpdateParentVolunteer(parentVolunteer, child);
                }
            }

            else
            {
                // If it's a new parent, add them
                AddParentVolunteer(application.ParentVolunteer);
            }

            // Assign a new application ID
            var max = 0;
            if (applicationCollection.Count(Builders<Application>.Filter.Empty) > 0)
            {
                max = applicationCollection.Find(Builders<Application>.Filter.Empty).SortByDescending(r => r.ApplicationId).Limit(1).ToList()[0].ApplicationId;
            }
            application.ApplicationId = max + 1;

            // Insert the application into the collection
            applicationCollection.InsertOne(application);
        }

        public void UpdateParentVolunteer(ParentVolunteer existingVolunteer, Child newChild)
        {
            if (!existingVolunteer.Children.Any(x => x.Name == newChild.Name))
            {
                existingVolunteer.Children.Add(newChild);
                var filter = Builders<Volunteer>.Filter.Eq(v => v.CrewNumber, existingVolunteer.CrewNumber);
                volunteerCollection.ReplaceOne(filter, existingVolunteer);
            }
        }

        public void AddYouthVolunteer(YouthVolunteer youthVolunteer)
        {
            var filter = Builders<Volunteer>.Filter.Eq(v => v.CrewNumber, youthVolunteer.CrewNumber);
            var existingVolunteer = volunteerCollection.Find(filter).FirstOrDefault();
            if (existingVolunteer == null)
            {
                var maxVolunteerId = 0;
                if (volunteerCollection.Count(Builders<Volunteer>.Filter.Empty) > 0)
                {
                    maxVolunteerId = volunteerCollection
                        .Find(Builders<Volunteer>.Filter.Empty)
                        .SortByDescending(p => p.VolunteerId)
                        .Limit(1)
                        .ToList()[0].VolunteerId;
                }
                youthVolunteer.VolunteerId = maxVolunteerId + 1;

                volunteerCollection.InsertOne(youthVolunteer);
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

