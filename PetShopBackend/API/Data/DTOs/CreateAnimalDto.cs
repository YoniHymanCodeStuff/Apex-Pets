using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.DTOs
{
    public class CreateAnimalDto
    {
        public string Species { get; set; }

        public string Category { get; set; }
        
        public string Required_Habitat { get; set; }

        public string Required_License { get; set; }

        public string Description { get; set; }

        public decimal price { get; set; }
    }
}