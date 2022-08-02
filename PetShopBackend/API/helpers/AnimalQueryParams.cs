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

        
         
         

        
        
    }
}