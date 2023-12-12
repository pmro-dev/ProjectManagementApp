using App.Features.Exceptions.Throw;

namespace App.Features.Pagination;

public class PaginationData
{
	public bool AreTherePagesToShow { get; }
	public bool IsCurrentPageFirstPage { get; }
	public bool IsThereNextPage { get; }
	public int CurrentPageNumber { get; }
	public int ItemsPerPageCount { get; }
	public int ItemsCount { get; }
	public int PagesCount { get; }
	public int NextPageNumber { get; }
	public int PreviousPageNumber { get; }

	private const int ValueOneIndicator = 1;
	private const int ValueZeroIndicator = 0;

	public PaginationData(int currentPageNumber, int itemsPerPageCount, int itemsCount, ILogger logger)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(PaginationData), currentPageNumber, ValueOneIndicator, nameof(currentPageNumber), logger);
		ExceptionsService.WhenValueNotInRangeThrow(nameof(PaginationData), itemsCount, ValueZeroIndicator, int.MaxValue, nameof(itemsCount), logger);
		ExceptionsService.WhenValueNotInRangeThrow(nameof(PaginationData), itemsPerPageCount, ValueOneIndicator, itemsCount, nameof(itemsPerPageCount), logger);

		CurrentPageNumber = currentPageNumber;
		ItemsPerPageCount = itemsPerPageCount;
		ItemsCount = itemsCount;
		NextPageNumber = currentPageNumber + ValueOneIndicator;
		PreviousPageNumber = currentPageNumber - ValueOneIndicator;

		PagesCount = PaginationHelper.CountPages(ItemsCount, ItemsPerPageCount, logger);
		ExceptionsService.WhenValueNotInRangeThrow(nameof(PaginationData), currentPageNumber, ValueOneIndicator, PagesCount, nameof(currentPageNumber), logger);
		
		AreTherePagesToShow = PaginationHelper.AreTherePagesToShow(PagesCount);
		IsCurrentPageFirstPage = PaginationHelper.IsCurrentPageFirstPage(currentPageNumber);
		IsThereNextPage = PaginationHelper.IsThereNextPage(NextPageNumber, PagesCount);
	}
}