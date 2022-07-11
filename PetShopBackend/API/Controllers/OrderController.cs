using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.UnitOfWork;
using API.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class OrderController : BaseApiController
    {
        private readonly IUoW _uow;
       public OrderController(IUoW uow)
       {
            _uow = uow;
        
       }
        
        //I load the user and the user has a list of animal Id that are his order. 
        //but then to display the actual cart I will need to get the details of the animals. 

        // [HttpGet("Cart")]
        // public async Task<ActionResult<ICollection<ShoppingCartItem>>> GetCustomerCart(string username){
        //     var cart = _uow.customers.GetCustomerAsync(username).
        // }

        [HttpPost("Cart")]
        public async Task<ActionResult> AddToCart(string username, int animalId)
        {
            var item = new ShoppingCartItem(){OrderedAnimalId = animalId};

            var user = (await _uow.customers.GetCustomerAsync(username)).Value;

            user.ShoppingCart.Add(item);

            _uow.customers.Update(user);

            if (await _uow.Complete()){
                return NoContent();
                
            }
            return BadRequest("Failed to add item to cart");
        }

        [HttpDelete("Cart")]
        public async Task<ActionResult> RemoveFromCart(string username, ShoppingCartItem item)
        {
            
            var user = (await _uow.customers.GetCustomerAsync(username)).Value;

            user.ShoppingCart.Remove(item);

            _uow.customers.Update(user); 

            if (await _uow.Complete()){
                return NoContent();
            }
            return BadRequest("Failed to remove item from cart");
        }

        [HttpPut("Checkout")]
        public async Task<ActionResult> Checkout(string username)
        {
            var user = (await _uow.customers.GetCustomerAsync(username)).Value;

            var AnimalData = await _uow.animals.GetAnimalsForCheckout(user);
                                 
            foreach (var item in user.ShoppingCart)
            {
                var animal = AnimalData.Where(x=>x.Id == item.OrderedAnimalId).SingleOrDefault();

                user.Orders.Append(new Order(){
                    OrderTimeStamp = DateTime.Now,
                    OrderStatus  = "Pending",
                    OrderedAnimalId = animal.Id,
                    OrderedAnimalSpecies = animal.Species,
                    price = animal.price
                });
            }

            user.ShoppingCart = new List<ShoppingCartItem>();

            _uow.customers.Update(user);

            if (await _uow.Complete()){
                return NoContent();
            }
            return BadRequest("Failed to checkout items");

        }


        // public async Task CommitOrder

        
        // public async Task EditOrderstatus


        //GetCustomerOrders
    }
}