
using API.Data.Model;

namespace API.Data.DTOs
{
    public class CartAnimalDto
    {
        
        public int cartItemId { get; set; }

        public int animalId {get;set;}
        public string Name { get; set; }

        public string Species { get; set; }
        public Photo MainPhoto {get;set; }



        public decimal price { get; set; }
                  
            
    }
}