using App.Features.Exceptions.Throw;

namespace App.Features.Pagination;

public class PaginationData
{
	public bool AreTherePagesToShow { get; }
	public bool IsNotCurrentPageFirstPage { get; }
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

		ItemsPerPageCount = itemsPerPageCount;
		ItemsCount = itemsCount;
		PagesCount = PaginationHelper.CountPages(ItemsCount, ItemsPerPageCount, logger);
		AreTherePagesToShow = PaginationHelper.AreTherePagesToShow(PagesCount);

		if (!AreTherePagesToShow) return;

		CurrentPageNumber = currentPageNumber > PagesCount ? PagesCount : currentPageNumber;
		NextPageNumber = CurrentPageNumber + ValueOneIndicator;
		PreviousPageNumber = CurrentPageNumber - ValueOneIndicator;

		IsNotCurrentPageFirstPage = PaginationHelper.IsNotCurrentPageFirstPage(CurrentPageNumber);
		IsThereNextPage = PaginationHelper.IsThereNextPage(NextPageNumber, PagesCount);
	}
}