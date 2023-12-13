using App.Features.Exceptions.Throw;
using System.Linq.Expressions;

namespace App.Features.Pagination;

public static class PaginationHelper
{
	private const int ValueOneIndicator = 1;
	private const int BottomBoundryForPage = 1;
	private const int BottomBoundryForItems = 0;

	public static int CountItemsToSkip(int pageNumber, int itemsPerPageCount, ILogger logger)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(CountItemsToSkip), pageNumber, nameof(pageNumber), logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(CountItemsToSkip), itemsPerPageCount, nameof(itemsPerPageCount), logger);

		int skipItemsCount = (pageNumber - ValueOneIndicator) * itemsPerPageCount;
		return skipItemsCount;
	}

	public static int CountPages(int itemsCount, int itemsPerPageCount, ILogger logger)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(CountPages), itemsPerPageCount, BottomBoundryForPage, nameof(itemsPerPageCount), logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(CountPages), itemsCount, BottomBoundryForItems, nameof(itemsCount), logger);

		return (int) Math.Ceiling((double) itemsCount / itemsPerPageCount);
	}

	public static bool AreTherePagesToShow(int pagesCount)
	{
		return pagesCount > ValueOneIndicator;
	}

	public static bool IsCurrentPageFirstPage(int currentPageNumber)
	{
		return currentPageNumber > ValueOneIndicator;
	}

	public static bool IsThereNextPage(int nextPageNumber, int pagesCount)
	{
		return nextPageNumber <= pagesCount;
	}
}

public static class IQueryableExtension
{
	public static IQueryable<T> UsePagination<T>(this IQueryable<T> source, Expression<Func<T, object>> orderBySelector, int pageNumber, int itemsPerPageCount, ILogger logger)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(IQueryableExtension), pageNumber, nameof(pageNumber), logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(IQueryableExtension), itemsPerPageCount, nameof(itemsPerPageCount), logger);

		int skipItemsCount = PaginationHelper.CountItemsToSkip(pageNumber, itemsPerPageCount, logger);

		var _items = source
				.OrderBy(orderBySelector)
				.Skip(skipItemsCount)
				.Take(itemsPerPageCount);

		return _items;
	}
}
