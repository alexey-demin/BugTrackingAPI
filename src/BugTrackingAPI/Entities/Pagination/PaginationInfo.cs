using System;

namespace BugTrackingAPI.Entities.Pagination
{
    public class PaginationInfo
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PaginationInfo(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
    }
}
