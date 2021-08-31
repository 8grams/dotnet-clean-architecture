namespace SFIDWebAPI.Application.Models.Query
{
    public class PaginationQuery : BaseQueryCommand
    {
        public int PagingPage { set; get; }
        public int PagingLimit { set; get; }
        public string SortColumn { set; get; }
        public string SortType { set; get; }
        public string QuerySearch { set; get; }
    }

    public class AdminPaginationQuery : BaseAdminQueryCommand
    {
        public int PagingPage { set; get; }
        public int PagingLimit { set; get; }
        public string SortColumn { set; get; }
        public string SortType { set; get; }
        public string QuerySearch { set; get; }
    }


}
