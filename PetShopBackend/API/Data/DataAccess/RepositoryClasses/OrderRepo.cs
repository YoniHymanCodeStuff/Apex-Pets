using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.DTOs;
using API.Data.Model;
using API.helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.DataAccess.RepositoryClasses
{
    public class OrderRepo:  Repository<Order>, IOrderRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        
        public OrderRepo(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<PagedList<OrderWithCustomerDto>> GetAllOrders(OrderQueryParams queryParams)
        {
            
            var query = _context.Orders.AsQueryable();

            if(queryParams.EarliestDate != null)
            {
                query = query.Where(x=>x.OrderTimeStamp>=queryParams.EarliestDate);
            }

            if(queryParams.LatestDate != null)
            {
                query = query.Where(x=>x.OrderTimeStamp<=queryParams.LatestDate);
            }
            
            if(!string.IsNullOrEmpty(queryParams.OrderStatus))
            {
                query = query.Where(x=>x.OrderStatus == queryParams.OrderStatus);
            }

            if(!string.IsNullOrEmpty(queryParams.SearchString))
            {
                var p = queryParams.SearchString.ToLower();
                
                query = query.Where(x=>x.customer.FirstName.ToLower().Contains(p) 
                    || x.customer.LastName.ToLower().Contains(p)
                    || x.customer.UserName.ToLower().Contains(p)
                    ||x.customer.Email.ToLower().Contains(p)
                    ||x.customer.Address.City.ToLower().Contains(p)
                    ||x.customer.Address.Street.ToLower().Contains(p));
                    
            }

             if(queryParams.OrderBy != null)
            {
                
                switch (queryParams.OrderBy)
                {
                    case "id":
                    query = query.OrderBy(x=>x.Id);
                    break;
           
                    case "species":
                    query =query.OrderBy(x=>x.OrderedAnimalSpecies);
                    break;

                    case "OrderedAnimalId":
                    query =query.OrderBy(x=>x.OrderedAnimalId);
                    break;
               
                    case "price":
                    query =query.OrderBy(x=>x.price);
                    break;

                    case "OrderStatus":
                    query = query.OrderBy(x=>x.OrderStatus);
                    break;

                    
                    case "OrderTimeStamp":
                    query = query.OrderBy(x=>x.OrderTimeStamp);
                    break;

                    case "DeliveryTimeStamp":
                    query = query.OrderBy(x=>x.DeliveryTimeStamp);
                    break;

                    case "LastName":
                    query = query.OrderBy(x=>x.customer.LastName);
                    break;

                    case "Username":
                    query = query.OrderBy(x=>x.customer.UserName);
                    break;

                    case "Customer":
                    query = query.OrderBy(x=>x.customer.Id);
                    break;

                    case "Address":
                    query = query.OrderBy(x=>x.customer.Address.City);
                    break;

                    default:
                    query = query.OrderBy(x=>x.OrderTimeStamp);
                    break;
                }
            }                     

            if(queryParams.IsDescending)
            {query =query.Reverse();}

            var mappedQuery = _mapper.ProjectTo<OrderWithCustomerDto>(query);
            

            return await PagedList<OrderWithCustomerDto>.CreateAsync
            (
                mappedQuery.AsNoTracking(),
                queryParams.Pagenumber,
                queryParams.PageSize
            );
            
        }

        public async Task<OrderWithCustomerDto> UpdateOrderStatus(orderStatusUpdateDto dto)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x=>x.Id == dto.order.OrderId);

            order.OrderStatus = dto.newStatus;

            if(dto.newStatus == "Delivered"){
                order.DeliveryTimeStamp = DateTime.Now;
            }

            var retOrder = dto.order;

            retOrder.DeliveryTimeStamp = order.DeliveryTimeStamp.ToString("dd/MM/yyyy HH:mm");
            retOrder.OrderStatus = dto.newStatus;

            return retOrder;

            // _context.Orders.Update(order);//should auto update
            
        }
    }
}