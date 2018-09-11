using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuffikKg.Models
{
    public class Puffik
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
