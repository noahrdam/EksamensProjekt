using Core.Model;
using MongoDB.Driver;
using ServerAPI.Repositories.Interfaces;

public class AdminRepository : IAdminRepository
{
    private readonly IMongoClient client;
    private readonly IMongoCollection<Application> applicationcollection;
    private readonly IMongoCollection<Event> eventcollection;
    private readonly IMongoCollection<Volunteer> volunteercollection;

    public AdminRepository()
    {
        var mongoUri = "mongodb+srv://eaa23fana:4321@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
        //var mongoUri = "mongodb://localhost:27017";
        client = new MongoClient(mongoUri);

        var database = client.GetDatabase("ChildClub");

        applicationcollection = database.GetCollection<Application>("Application");
        eventcollection = database.GetCollection<Event>("Event");
        volunteercollection = database.GetCollection<Volunteer>("Volunteer");
    }

    public List<Application> GetAllApplication()
    {
        return applicationcollection.Find(Builders<Application>.Filter.Empty).ToList();
    }

    public List<Event> GetAllEvents()
    {
        return eventcollection.Find(Builders<Event>.Filter.Empty).ToList();
    }


	public void UpdateStatus(Application application)
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


	public List<Application> GetFilteredApplicationsByWeek(int week)
	{
        var filter = Builders<Application>.Filter.Or(
            Builders<Application>.Filter.And(
                Builders<Application>.Filter.Eq(ap => ap.FirstPrio.Week, week),
                Builders<Application>.Filter.Eq(ap => ap.Status, "1.Prioritet")
            ),
            Builders<Application>.Filter.And(
                Builders<Application>.Filter.Eq(ap => ap.SecondPrio.Week, week),
                Builders<Application>.Filter.Eq(ap => ap.Status, "2.Prioritet")
            )
        );

        return applicationcollection.Find(filter).ToList();
    }

    public List<Application> GetFilteredApplicationsByPeriod(int week, string from, string to)
    {
        var filter = Builders<Application>.Filter.Or(
            Builders<Application>.Filter.And(
                Builders<Application>.Filter.Eq(ap => ap.FirstPrio.Week, week),
                Builders<Application>.Filter.Eq(ap => ap.FirstPrio.From, from),
                Builders<Application>.Filter.Eq(ap => ap.FirstPrio.To, to),
                Builders<Application>.Filter.Eq(ap => ap.Status, "1.Prioritet")
            ),
            Builders<Application>.Filter.And(
                Builders<Application>.Filter.Eq(ap => ap.SecondPrio.Week, week),
                Builders<Application>.Filter.Eq(ap => ap.SecondPrio.From, from),
                Builders<Application>.Filter.Eq(ap => ap.SecondPrio.To, to),
                Builders<Application>.Filter.Eq(ap => ap.Status, "2.Prioritet")
            )
        );

        return applicationcollection.Find(filter).ToList();
    }

    public void PublishAllApplications(List<Application> applications)
    {
        foreach (var application in applications)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.ApplicationId, application.ApplicationId);
            var update = Builders<Application>.Update.Set(a => a.IsPublished, true);
            applicationcollection.UpdateOneAsync(filter, update);
        }
    }
}
