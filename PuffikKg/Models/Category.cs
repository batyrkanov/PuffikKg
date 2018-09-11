using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuffikKg.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Puffik> Puffiks { get; set; }

        public Category()
        {
            Puffiks = new List<Puffik>();
        }
    }
}
