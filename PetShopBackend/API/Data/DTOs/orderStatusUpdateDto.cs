using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.DTOs
{
    public class orderStatusUpdateDto
    {
        public string newStatus { get; set; }
        public OrderWithCustomerDto order { get; set; }
    }
}