using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.UnitOfWork;
using API.Data.DTOs;
using API.Data.Model;
using API.Extensions;
using API.helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class OrderController : BaseApiController
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;
        public OrderController(IUoW uow, IMapper mapper)
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
        public async Task<ActionResult<CartAnimalDto>> AddToCart(addToCartDto dto)
        {
            var item = new ShoppingCartItem() { OrderedAnimalId = dto.animalId };

            var user = (await _uow.customers.GetCustomerAsync(dto.username)).Value;



            user.ShoppingCart.Add(item);

            _uow.customers.Update(user);

            var cartAnimalDto = await _uow.animals.GetCartAnimal(item);

            if (await _uow.Complete())
            {

                return cartAnimalDto;

            }
            return BadRequest("Failed to add item to cart");
        }

        [HttpPut("Cart-Remove")]
        public async Task<ActionResult> RemoveFromCart(CartRemoveDto dto)
        {

            var username = User.GetUserName();

            //I don't need the dto as the arg here, just a shopping cart item. 
            //should fix that at cleanup. 

            var user = (await _uow.customers.GetCustomerForUpdates(username)).Value;

            //var parsedItem = (ShoppingCartItem)dto.item;

            var itemToRemove = user.ShoppingCart.Where(x => x.Id == dto.item.Id).SingleOrDefault();

            user.ShoppingCart.Remove(itemToRemove);

            _uow.customers.Update(user);

            if (await _uow.Complete())
            {
                return NoContent();
            }
            return BadRequest("Failed to remove item from cart");
        }

        [HttpDelete("Checkout")]
        public async Task<ActionResult> Checkout()
        {
            var username = User.GetUserName();

            var user = (await _uow.customers.GetCustomerForUpdates(username)).Value;

            var AnimalData = await _uow.animals.GetAnimalsForCheckout(user);

            List<ShoppingCartItem> cartList = user.ShoppingCart.ToList();

            for (var i = 0; i < cartList.Count; i++)
            {
                var item = cartList[i];
                var animal = AnimalData.Where(x => x.Id == item.OrderedAnimalId).SingleOrDefault();

                user.Orders.Add(new Order()
                {
                    OrderTimeStamp = DateTime.Now,
                    OrderStatus = "Pending",
                    OrderedAnimalId = animal.Id,
                    OrderedAnimalName = animal.Name,
                    price = animal.price
                });

            }

            user.ShoppingCart.Clear();

            _uow.customers.Update(user);

            if (await _uow.Complete())
            {
                return NoContent();
            }
            return BadRequest("Failed to checkout items");

        }

        [HttpGet("CartAnimals")]
        public async Task<ActionResult<ICollection<CartAnimalDto>>> GetCartAnimalsAsync()
        {

            var username = User.GetUserName();

            var cartAnimals = await _uow.animals.GetCartAnimals(username);

            return Ok(cartAnimals);

        }

        [HttpGet("orders")]
        public async Task<ActionResult<ICollection<OrderDto>>> GetOrders()
        {

            var username = User.GetUserName();

            var orders = await _uow.customers.GetCustomerOrders(username);

            var orderDtos = _mapper.Map<ICollection<OrderDto>>(orders);

            return Ok(orderDtos);

        }

        [HttpGet("allOrders")]
        public async Task<ActionResult<PagedList<OrderWithCustomerDto>>> GetAllOrders([FromQuery] OrderQueryParams queryParams)
        {

            if(!User.GetIsAdmin()){return BadRequest("Only admins may access this data.");}
            
            var orders = await _uow.orders.GetAllOrders(queryParams);

            Response.AddPaginationHeader(orders.CurrentPage, orders.Pagesize, orders.TotalItems, orders.TotalPages);

            return Ok(orders);

        }

        [HttpPatch("updateStatus")]
        public async Task<ActionResult<OrderWithCustomerDto>> UpdateOrderStatus(orderStatusUpdateDto dto)
        {
            if(!User.GetIsAdmin()){return BadRequest("Only admins can execute this action.");}


            var updated = await _uow.orders.UpdateOrderStatus(dto);

            if (await _uow.Complete())
            {
                return updated;
            }
            return BadRequest("Failed to update order status");

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