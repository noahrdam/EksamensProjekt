﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public Volunteer Volunteer { get; set; } = new Volunteer();

        public Event FirstPrio { get; set; } = new Event();

        public Event SecondPrio { get; set; } = new Event();
	}
}
