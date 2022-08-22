using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.helpers
{
    public class OrderQueryParams
    {
        private const int _maxPageSize = 50; 

        private int _pageSize = 10; 
        public int Pagenumber { get; set; }
        public int PageSize{
            get=>_pageSize;
            set=> _pageSize =  Math.Min(_maxPageSize,value);
            }

        public string SearchString { get; set; }
        public bool IsDescending { get; set; } = false; 
        public string OrderBy { get; set; } = "OrderTimeStamp";

        public DateTime? EarliestDate {get;set;}
        public DateTime? LatestDate {get;set;} 

        public string OrderStatus {get;set;}
        
        
    }
}