namespace Shared
{
    public record PaginatedResult<TData>(int pageIndex,int pageSize,int totalCount,IEnumerable<TData> data);
}
