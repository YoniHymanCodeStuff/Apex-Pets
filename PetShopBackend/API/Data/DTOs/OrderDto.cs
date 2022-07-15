using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.DTOs
{
    public class OrderDto
    {
        public string OrderTimeStamp { get; set; }
        public string OrderStatus { get; set; }
        
        public string DeliveryTimeStamp { get; set; }

        public int OrderedAnimalId { get; set; }

        public string OrderedAnimalSpecies {get;set;}

        public decimal price { get; set; }
    }
}