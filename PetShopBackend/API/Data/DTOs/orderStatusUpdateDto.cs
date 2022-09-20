

namespace API.Data.DTOs
{
    public class orderStatusUpdateDto
    {
        public string newStatus { get; set; }
        public OrderWithCustomerDto order { get; set; }
    }
}