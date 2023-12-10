using App.Features.Exceptions.Throw;
using System.Linq.Expressions;

namespace App.Features.Pagination;

public static class PaginationHelper
{
	private const int ValueOneIndicator = 1;

	public static int CountItemsToSkip(int pageNumber, int itemsPerPageCount, ILogger logger)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(CountItemsToSkip), pageNumber, nameof(pageNumber), logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(CountItemsToSkip), itemsPerPageCount, nameof(itemsPerPageCount), logger);

		int skipItemsCount = (pageNumber - ValueOneIndicator) * itemsPerPageCount;
		return skipItemsCount;
	}

	//public static int CountItemsLeftToTake(int collectionSize, int skipCount, int itemsPerPageCount, ILogger logger)
	//{
	//	double temp = (collectionSize / skipCount);

	//	int itemsToTakeCount = Convert.ToInt32(Math.Ceiling(temp));

	//	return itemsToTakeCount;
	//}
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
