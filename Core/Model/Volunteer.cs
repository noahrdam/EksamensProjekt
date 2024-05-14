using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Volunteer
    {
        public ObjectId Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }

        public int CrewNumber { get; set; }

        public string Mail {  get; set; }

		public List<Child> Children { get; set; } = new List<Child>();

        public bool IsYouth { get; set; } = false;
}
}
