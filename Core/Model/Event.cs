using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Core.Model
{
    public class Event
    {
        public ObjectId Id { get; set; }

        public int EventId { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public int Week { get; set; }

        public string Location { get; set; }
    }
}
