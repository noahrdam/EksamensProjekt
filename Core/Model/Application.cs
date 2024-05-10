using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Application
    {

        public ObjectId Id { get; set; }
        public int ApplicationId { get; set; }

        public bool BeenBefore { get; set; }

        public string Comment { get; set; }

        public string ConsentForm { get; set; }

        public Parent Parent { get; set; } = new Parent();

		public List<Child> Children { get; set; } = new List<Child>();

		// public Admin Admin { get; set; }    

		public Event FirstPrio { get; set; } = new Event();

		public Event SecondPrio { get; set; } = new Event();
	}
}
