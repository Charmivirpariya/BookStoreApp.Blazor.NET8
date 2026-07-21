namespace BookStoreApp.API.Models
{
    public class QueryParameter
    {
        private int pageSize = 15;
        public int StartIndex {  get; set; }

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
