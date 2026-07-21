namespace BookStoreAppBlazor.Server.UI.Models
{
    public class QueryParameters
    {
        public int pageSize { get; set; }
        public int StartIndex { get; set; }
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
    }
}
