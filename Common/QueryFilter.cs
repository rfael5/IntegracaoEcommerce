public class QueryFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 100;
    public string? SortBy { get; set; }
    public string? Search { get; set; }
}