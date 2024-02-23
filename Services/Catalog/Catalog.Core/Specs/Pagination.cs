using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Specs
{
    public class Pagination<T> where T : class
    {
        public Pagination()
        {

        }

        public Pagination(int pageNumber, int pageSize, int count, IReadOnlyCollection<T> items)
        {
            PageIndex = pageNumber;
            PageSize = pageSize;
            Count = count;
            Data = items;
        }

        public int PageIndex { get; set; }  
        public int PageSize { get; set; }   
        public long Count { get; set; } 
        public IReadOnlyCollection<T> Data { get; set; }
    }
}
