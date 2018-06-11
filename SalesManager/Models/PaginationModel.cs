using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class PaginationModel
    {
        public int TotalPage { get; set; }

        public int PageSize { get; set; }

        public int LinkLimit { get; set; }

        public int PageIndex { get; set; }

        public string UrlPaging { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int PageCount
        {
            get { return TotalPage > 0 && PageSize > 0 ? (int)Math.Ceiling(TotalPage / (double)PageSize) : 0; }
        }

    }
}