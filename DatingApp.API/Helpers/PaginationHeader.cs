using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Helpers
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PaginationHeader(int curent, int items,int total, int totalPages)
        {
            this.CurrentPage = curent;
            this.ItemsPerPage = items;
            this.TotalItems = total;
            this.TotalPages = totalPages;
        }
    }
}
