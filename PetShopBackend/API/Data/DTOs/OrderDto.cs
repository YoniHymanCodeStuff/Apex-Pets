
namespace API.Data.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderTimeStamp { get; set; }
        public string OrderStatus { get; set; }
        
        public string DeliveryTimeStamp { get; set; }

        public int OrderedAnimalId { get; set; }

        public string OrderedAnimalName {get;set;}

        public decimal price { get; set; }
    }
}