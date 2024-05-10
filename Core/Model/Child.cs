using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Child
    {
        public ObjectId Id { get; set; }
        public int ChildId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ClothingSize { get; set; }
        public string Interests {  get; set; }
    }
}
