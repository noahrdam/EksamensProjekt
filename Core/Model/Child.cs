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
        [Required(ErrorMessage = "Navn skal angives")]
        public string Name { get; set; }

        [Range(5, 18, ErrorMessage = "Børneklubben er kun tilgængelige for børn mellem 5 - 18 år.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Tøjstørrelse skal angives")]
        public string ClothingSize { get; set; }
        [Required(ErrorMessage = "Interesser skal angives")]
        public string Interests {  get; set; }
    }
}
