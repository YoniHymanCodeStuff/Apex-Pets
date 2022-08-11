using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.helpers
{
    public class AnimalQueryParams
    {
        private const int _maxPageSize = 50; 

        private int _pageSize = 10; 
        public int Pagenumber { get; set; }
        public int PageSize{
            get=>_pageSize;
            set=> _pageSize =  Math.Min(_maxPageSize,value);
            }

        
         public string Category { get; set; }
         public decimal? MaxPrice { get; set; }
         public decimal? MinPrice { get; set; }
         
         public string OrderBy { get; set; } = "id";
         public bool IsDescending { get; set; } 

         public string SearchString { get; set; } 
    }
}