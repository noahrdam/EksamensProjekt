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
        //var mongoUri = "mongodb+srv://eaa23fana:4321@childclubdb.qdo9bmh.mongodb.net/?retryWrites=true&w=majority&appName=ChildClubDB";
        var mongoUri = "mongodb://localhost:27017";
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

	public async Task UpdateStatus(Application application)
	{
		var filter = Builders<Application>.Filter.Eq(a => a.ApplicationId, application.ApplicationId);
		var update = Builders<Application>.Update.Set(a => a.Status, application.Status);
		await applicationcollection.UpdateOneAsync(filter, update);
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

    public Application GetApplicationById(int id)
    {
        var filter = Builders<Application>.Filter.Eq(a => a.ApplicationId, id);
        return applicationcollection.Find(filter).FirstOrDefault();
    }

    public List<ParentVolunteer> GetNewsGroup()
    {
        var filter = Builders<Volunteer>.Filter.Eq("newsgroup", true);
        var volunteers = volunteercollection.Find(filter).ToList();
        return volunteers.ConvertAll(v => (ParentVolunteer)v);
    }
}
