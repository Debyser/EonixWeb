using EonixWebApi.ApplicationCore.Entities.Enums;

namespace EonixWebApi.ApplicationCore.Entities
{
    public class PageRequest
    {
        public int? PageSize { get; set; }
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
        public SortDirection Direction { get; set; } = SortDirection.Ascending;
    }
}
