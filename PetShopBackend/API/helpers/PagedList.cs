using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int Pagesize { get; set; }

        public int TotalItems { get; init; }
        
        
        public PagedList(IEnumerable<T> items,
        int count, int pageNumber, int pageSize  )
        {
            this.CurrentPage = pageNumber;
            this.Pagesize = pageSize;
            this.TotalPages = (int) Math.Ceiling( count/(float)pageSize);
            AddRange(items);//this adds it into the class itself or something.
            this.TotalItems = count;
        }
        
        
        public static async Task<PagedList<T>> CreateAsync( IQueryable<T> source, int pageNumber,int pageSize){
           
           var count = await source.CountAsync();
           var items = await source.Skip((pageNumber-1)*pageSize)
           .Take(pageSize).ToListAsync();

           return new PagedList<T>(items,count,pageNumber,pageSize);
        }
        
        
    }
}