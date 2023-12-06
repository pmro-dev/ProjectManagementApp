namespace App.Features.Pagination;

public class PaginationList<T> : List<T> where T : class
{
	public List<T> Items { get; set; }

    public PaginationList(IQueryable<T> items, int pageNumber, int itemsPerPageCount)
    {
		int itemsCountToSkip = pageNumber * itemsPerPageCount;

		Items = items.Skip(itemsCountToSkip).Take(itemsPerPageCount).ToList();
	}
}
