using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Common
{
    public class PaginationResult<T>
    {
        public PaginationResult(IReadOnlyList<T> data, int totalElements, int pageSize, int pageNumber)
        {
            Data = data;
            TotalCount = totalElements;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public IReadOnlyList<T> Data { get; set; } = default!;
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages =>(int)Math.Ceiling((double)TotalCount / PageSize); 

    }
}
