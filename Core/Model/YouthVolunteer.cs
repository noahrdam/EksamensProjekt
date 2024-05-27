using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;



namespace Core.Model
{
    [BsonDiscriminator("YouthVolunteer")]
    public class YouthVolunteer : Volunteer
    {
        
        public string ConsentForm { get; set; }
        [Required(ErrorMessage = "Forældres navn skal angives")]
        public string ParentName { get; set; }

        public DateTime DateOfConsent { get; set; } = DateTime.Now;

        public YouthVolunteer()
        {
            DateOfConsent = DateTime.Now;
        }
    }
}
