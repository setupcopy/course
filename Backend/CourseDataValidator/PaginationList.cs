using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseUtility
{
    public class PaginationList<T>:List<T>
    {
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginationList(int totalCount,int currentPage,int pageSize,List<T> items)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PaginationList<T>> CreateAsync(int currentPage,int pageSize,IQueryable<T> result)
        {
            var totalCount = await result.CountAsync();
            //分页
            var skip = (currentPage - 1) * pageSize;
            result = result.Skip<T>(skip).Take<T>(pageSize);
            //以pageSize为标准显示一定量的数据
            //result = result.Take(pageSize);

            var items = await result.ToListAsync();

            return new PaginationList<T>(totalCount,currentPage,pageSize,items);
        }
    }
}
