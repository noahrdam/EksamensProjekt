//using Core.Model;
//using MongoDB.Driver;
//using MongoDB.Bson;
//using ServerAPI.Repositories.Interfaces;

//namespace ServerAPI.Repositories
//{
//    public class RegistrationRepository : IRegistrationRepository
//    {
//        private IMongoClient client;
//        private IMongoCollection<Application> applicationcollection;
//        private IMongoCollection<Volunteer> volunteercollection;

//        public RegistrationRepository()
//        {
//            //var password = "4321";
//            //var mongoUri = $"mongodb+srv://eaa23fana:{password}@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
//            var mongoUri = "mongodb://localhost:27017";


//            try
//            {
//                client = new MongoClient(mongoUri);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("There was a problem connecting to your " +
//                    "Atlas cluster. Check that the URI includes a valid " +
//                    "username and password, and that your IP address is " +
//                    $"in the Access List. Message: {e.Message}");
//                Console.WriteLine(e);
//                Console.WriteLine();
//                return;
//            }


//            var dbName = "ChildClub";
//            var ApplicationCollectionName = "Application";
//            var VolunteerCollectionName = "Volunteer";
//            var ChildrenCollectionName = "Children"; // Name for the children collection


//            applicationcollection = client.GetDatabase(dbName).GetCollection<Application>(ApplicationCollectionName);

//            volunteercollection = client.GetDatabase(dbName).GetCollection<Volunteer>(VolunteerCollectionName);

//        }

//        public void AddParentVolunteer(ParentVolunteer volunteer)
//        {
//            var existingParent = volunteercollection.Find(p => p.CrewNumber == volunteer.CrewNumber).FirstOrDefault();
//            if (existingParent == null)
//            {
//                var maxVolunteerId = 0;
//                if (volunteercollection.Count(Builders<Volunteer>.Filter.Empty) > 0)
//                {
//                    maxVolunteerId = volunteercollection
//                        .Find(Builders<Volunteer>.Filter.Empty)
//                        .SortByDescending(p => p.VolunteerId)
//                        .Limit(1)
//                        .Limit(1)
//                        .ToList()[0]
//                        .VolunteerId;
//                }
//                volunteer.VolunteerId = maxVolunteerId + 1;

//                volunteercollection.InsertOne(volunteer);
//            }
//        }

//        public void RegisterApplication(Application application)
//        {
//            var volunteer = volunteercollection.Find(p => p.CrewNumber == application.ParentVolunteer.CrewNumber).FirstOrDefault();

//            if (volunteer != null)
//            {
//                application.ParentVolunteer.VolunteerId = volunteer.VolunteerId;  // Link the application to the existing parent ID

//                // Check if adding a new child would exceed the limit of 2 children per parent
//                if (application.ParentVolunteer.Children.Count < 2)
//                {
//                    // Update parent with the new child only if they have fewer than 2 children
//                    UpdateVolunteer(application.ParentVolunteer, application.ParentVolunteer.Children[0]);
//                }
//                else
//                {
//                    // Optionally, handle the case where a parent already has 2 children and is attempting to add more
//                    throw new Exception("A parent can only apply for two children.");
//                }
//            }
//            else
//            {
//                // If it's a new parent, add them
//                AddParentVolunteer(application.ParentVolunteer);
//            }

//            // Assign a new application ID
//            var max = 0;
//            if (applicationcollection.Count(Builders<Application>.Filter.Empty) > 0)
//            {
//                max = applicationcollection.Find(Builders<Application>.Filter.Empty).SortByDescending(r => r.ApplicationId).Limit(1).ToList()[0].ApplicationId;
//            }
//            application.ApplicationId = max + 1;

//            // Insert the application into the collection
//            applicationcollection.InsertOne(application);
//        }


//        public void UpdateVolunteer(ParentVolunteer existingVolunteer, Child newChild)
//        {
//            if (!existingVolunteer.Children.Any(c => c.Name == newChild.Name))
//            {
//                existingVolunteer.Children.Add(newChild);
//                volunteercollection.ReplaceOne(p => p.CrewNumber == existingVolunteer.CrewNumber, existingVolunteer);
//            }
//        }

//        public void AddYouthVolunteer(YouthVolunteer youthVolunteer)
//        {
//            var existingVolunteer = volunteercollection.Find(v => v.CrewNumber == youthVolunteer.CrewNumber).FirstOrDefault();
//            if (existingVolunteer == null)
//            {
//                var maxVolunteerId = 0;
//                if (volunteercollection.Count(Builders<Volunteer>.Filter.Empty) > 0)
//                {
//                    maxVolunteerId = volunteercollection
//                        .Find(Builders<Volunteer>.Filter.Empty)
//                        .SortByDescending(p => p.VolunteerId)
//                        .Limit(1)
//                        .ToList()[0]
//                        .VolunteerId;
//                }
//                youthVolunteer.VolunteerId = maxVolunteerId + 1;

//                volunteercollection.InsertOne(youthVolunteer);
//            }
//        }

//        public List<Event> GetEvents()
//        {
//            var dbName = "ChildClub";
//            var collectionName = "Event";

//            var eventCollection = client.GetDatabase(dbName)
//               .GetCollection<Event>(collectionName);

//            return eventCollection.Find(Builders<Event>.Filter.Empty).ToList();
//        }

//    }
//}

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
            var mongoUri = "mongodb+srv://eaa23fana:4321@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
            //var mongoUri = "mongodb://localhost:27017";

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
            var existingParent = volunteerCollection.Find(x => x.CrewNumber == parentVolunteer.CrewNumber).FirstOrDefault();
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
            var volunteer = volunteerCollection.Find(x => x.CrewNumber == application.ParentVolunteer.CrewNumber).FirstOrDefault();

            if (volunteer is ParentVolunteer parentVolunteer)
            {
                application.ParentVolunteer.VolunteerId = parentVolunteer.VolunteerId;  // Link the application to the existing parent ID

                // Check if adding a new child would exceed the limit of 2 children per parent
                if (parentVolunteer.Children.Count < 2)
                {
                    // Update parent with the new child only if they have fewer than 2 children
                    UpdateParentVolunteer(parentVolunteer, application.ParentVolunteer.Children[0]);
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
                volunteerCollection.ReplaceOne(p => p.CrewNumber == existingVolunteer.CrewNumber, existingVolunteer);
            }
        }

        public void AddYouthVolunteer(YouthVolunteer youthVolunteer)
        {
            var existingVolunteer = volunteerCollection.Find(x => x.CrewNumber == youthVolunteer.CrewNumber).FirstOrDefault();
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
