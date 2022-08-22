using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DTOs;
using API.Data.Model;
using API.helpers;

namespace API.Data.DataAccess.RepositoryInterfaces
{
    public interface IOrderRepo : IRepository<Order>
    {
        Task<PagedList<OrderWithCustomerDto>> GetAllOrders(OrderQueryParams queryParams); 
        
        Task<OrderWithCustomerDto> UpdateOrderStatus(orderStatusUpdateDto dto);
    }
}