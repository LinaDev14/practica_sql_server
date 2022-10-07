using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseDeDatos
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }   
        

        // creación del constructor
        public Beer(int id, string name, int brandId)
        {
            Id = id;
            Name = name;
            BrandId = brandId;
        }

        public Beer(string name, int brandId)
        {
            
            Name = name;
            BrandId = brandId;
        }
    }
}
