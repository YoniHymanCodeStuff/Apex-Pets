using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.UnitOfWork;
using API.Data.DTOs;
using API.Data.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class OrderController : BaseApiController
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;
       public OrderController(IUoW uow,IMapper mapper)
       {
            _mapper = mapper;
            _uow = uow;
        
       }
        
        //I load the user and the user has a list of animal Id that are his order. 
        //but then to display the actual cart I will need to get the details of the animals. 

        // [HttpGet("Cart")]
        // public async Task<ActionResult<ICollection<ShoppingCartItem>>> GetCustomerCart(string username){
        //     var cart = _uow.customers.GetCustomerAsync(username).
        // }


        [HttpPost("Cart-Add")]
        public async Task<ActionResult> AddToCart(addToCartDto dto)
        {
            var item = new ShoppingCartItem(){OrderedAnimalId = dto.animalId};

            var user = (await _uow.customers.GetCustomerAsync(dto.username)).Value;

            

            user.ShoppingCart.Add(item);

            _uow.customers.Update(user);

            if (await _uow.Complete()){
                return NoContent();
                
            }
            return BadRequest("Failed to add item to cart");
        }

        [HttpPut("Cart-Remove")]
        public async Task<ActionResult> RemoveFromCart(CartRemoveDto dto)
        {
            
            var user = (await _uow.customers.GetCustomerForUpdates(dto.username)).Value;

            //var parsedItem = (ShoppingCartItem)dto.item;

            var itemToRemove = user.ShoppingCart.Where(x=>x.Id == dto.item.Id).SingleOrDefault();
            
            user.ShoppingCart.Remove(itemToRemove);

            _uow.customers.Update(user); 

            if (await _uow.Complete()){
                return NoContent();
            }
            return BadRequest("Failed to remove item from cart");
        }

        [HttpDelete("Checkout/{username}")]
        public async Task<ActionResult> Checkout(string username)
        {
            
            var user = (await _uow.customers.GetCustomerForUpdates(username)).Value;

            var AnimalData = await _uow.animals.GetAnimalsForCheckout(user);

            List<ShoppingCartItem> cartList = user.ShoppingCart.ToList();
            
            for (var i = 0; i < cartList.Count; i++)
            {
                var item = cartList[i];
                var animal = AnimalData.Where(x=>x.Id == item.OrderedAnimalId).SingleOrDefault();

                user.Orders.Add(new Order(){
                    OrderTimeStamp = DateTime.Now,
                    OrderStatus  = "Pending",
                    OrderedAnimalId = animal.Id,
                    OrderedAnimalSpecies = animal.Species,
                    price = animal.price
                });
                
            }

            user.ShoppingCart.Clear();

            _uow.customers.Update(user);

            if (await _uow.Complete()){
                return NoContent();
            }
            return BadRequest("Failed to checkout items");

        }

        [HttpGet("CartAnimals/{customerName}")]
        public async Task<ActionResult<ICollection<CartAnimalDto>>> GetCartAnimalsAsync(string customerName)
        {
            //this is not secure currently... 
            
            var cartAnimals = await _uow.animals.GetCartAnimals(customerName);
            
            return Ok(cartAnimals);

        }

        [HttpGet("orders/{customerName}")]
        public async Task<ActionResult<ICollection<OrderDto>>> GetOrders(string customerName)
        {
            //this is not secure currently... 
            
            var orders = await _uow.customers.GetCustomerOrders(customerName);
            
            var orderDtos = _mapper.Map<ICollection<OrderDto>>(orders);

            return Ok(orderDtos);

        }


        //is this neccessary? I just get this all when I get the user. which
        //I probably should have anyways... 
        // [HttpGet("{username}")]
        // public async Task<ActionResult<ICollection<Order>>> GetCustomerOrders(string username)
        // {
        //     var customer = 
        // }
               
        // public async Task EditOrderstatus


        
    }
}