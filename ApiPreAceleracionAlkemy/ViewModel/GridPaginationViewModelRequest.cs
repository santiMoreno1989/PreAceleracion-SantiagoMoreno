namespace ApiPreAceleracionAlkemy.ViewModel
{
    public class GridPaginationViewModelRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortColumnName { get; set; }  
        public string SortColumnDirection { get; set; }

    }
}
