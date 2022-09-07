using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public class Order
    {
        
        public int Id { get; set; }

        public Customer customer {get;set;}
                
        public DateTime OrderTimeStamp { get; set; }
        public string OrderStatus { get; set; }//pending/delivered - should this be bool or are there more states?
        
        public DateTime DeliveryTimeStamp { get; set; }

        public int OrderedAnimalId { get; set; }

        public string OrderedAnimalName {get;set;}

        public decimal price { get; set; }
        
                
    }
}