using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Child
    {
        public string Name { get; set; }

        [Range(5, 18, ErrorMessage = "Børneklubben er kun tilgængelige for børn mellem 5 - 18 år.")]
        public int Age { get; set; }
        public string ClothingSize { get; set; }
        public string Interests {  get; set; }
    }
}
