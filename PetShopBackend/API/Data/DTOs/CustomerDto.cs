using System.Collections.Generic;
using API.Data.Model;

namespace API.Data.DTOs
{
    public class CustomerDto : BaseUserDto
    {

        public string CreditInfo { get; set; }
       
        public DeliveryAdress Address {get;set;}

         public ICollection<Order> Orders {get;set;}
         public ICollection<ShoppingCartItem> ShoppingCart {get;set;}

         public int PhoneNumber { get; set; }
    }
}