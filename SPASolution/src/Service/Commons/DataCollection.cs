using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commons
{
    public static class Paging
    {
        public static async Task<DataCollection<T>> PagedAsync<T>(
            this IQueryable<T> query,
            int page,
            int take
        ) where T : class
        {
            var result = new DataCollection<T>();

            result.Total = await query.CountAsync();
            result.Page = page;

            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(
                    Math.Ceiling(
                        Convert.ToDecimal(result.Total) / take
                    )
                );

                result.Items = await query.Skip((page - 1) * take)
                                            .Take(take)
                                            .ToListAsync();
            }

            return result;
        }
    }
    public class DataCollection<T> where T : class
    {
        public bool HasItems
        {
            get
            {
                return Items != null && Items.Any();
            }
        }
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
    }
}
