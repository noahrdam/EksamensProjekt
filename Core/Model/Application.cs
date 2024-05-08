using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public bool BeenBefore { get; set; }

        public string Comment { get; set; }

        public string ConsentForm { get; set; }

        public Parent Parent { get; set; }

        public Admin Admin { get; set; }    

        public Event FirstPrio { get; set; }

        public Event SecondPrio { get; set; }
    }
}
