using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Model;

namespace API.Data
{
    public class Animal 
    {

        public int Id { get; set; }
        public string Species { get; set; }

        public string Category { get; set; }
        public Photo MainPhoto {get;set; }

        public string Required_Habitat { get; set; }

        public string Required_License { get; set; }

        public string Description { get; set; }

        public ICollection<Photo> images {get;set;}

        //forgot price
        

    }
}