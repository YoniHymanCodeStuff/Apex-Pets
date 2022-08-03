using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.helpers;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(
            this HttpResponse response,
            int currentPage,
             int itemsPerPage,
             int totalItems,
             int totalPages ){
            
            var header = new PaginationHeader(currentPage,itemsPerPage,totalItems,totalPages);
            var options = new  JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("pagination",JsonSerializer.Serialize(header,options));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");//this is telling cors to allow this header access. 

        }
    }
}