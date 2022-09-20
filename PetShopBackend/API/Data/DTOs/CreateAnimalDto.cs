namespace API.Data.DTOs
{
    public class CreateAnimalDto
    {
        public string Name { get; set; }

        public string Species { get; set; }
        public string Category { get; set; }
        
        public string Required_Habitat { get; set; }

        

        public string Description { get; set; }

        public decimal price { get; set; }
    }
}