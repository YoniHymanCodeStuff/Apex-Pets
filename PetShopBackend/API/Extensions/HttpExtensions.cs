
using System.Text.Json;
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
            response.Headers.Add("Access-Control-Expose-Headers","Pagination"); 

        }
    }
}