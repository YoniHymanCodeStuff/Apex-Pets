using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public class DeliveryAdress
    {
        public int Id {get;set;}
        public string Country {get;set;} = "Peru";
        public string City { get; set; } = "Hong Kong";
        public string Street { get; set; }= "Baker street";

        public int houseNumber {get;set;}= 221;

        public string Zip {get;set;}
    }
}