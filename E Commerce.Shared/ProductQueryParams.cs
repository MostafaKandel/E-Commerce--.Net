using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public class ProductQueryParams
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? search { get; set; }

        public ProductSortingOptions Sort { get; set; }

        private int _pageIndex = 1;
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                if (value < 1)
                    _pageIndex = 1;
                else
                    _pageIndex = value;
            }
        }
    
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        private int _pageSize = DefaultPageSize;
        
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if(value <=0)
                    _pageSize = DefaultPageSize;
                else if (value > MaxPageSize)
                    _pageSize = MaxPageSize;
                else
                    _pageSize = value;
            }
        }
    }
}
