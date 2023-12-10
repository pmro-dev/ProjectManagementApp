using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Briefly;

public static class TupleBrieflyMapper
{
	public static List<Tuple<TodoListDto, int, int>> MapToDto(List<Tuple<TodoListModel, int, int>> tupleDatas)
	{
		var tupleDataDtos = new List<Tuple<TodoListDto, int, int>>();

		using (var scoped = new HttpContextAccessor().HttpContext?.RequestServices.CreateScope() ?? throw new InvalidOperationException(""))
		{
			var todoListMapper = scoped.ServiceProvider.GetRequiredService<ITodoListMapper>();

			Parallel.ForEach(tupleDatas, tuple =>
			{
				tupleDataDtos.Add(
					Tuple
					.Create(
						todoListMapper.TransferToDto(tuple.Item1),
						tuple.Item2,
						tuple.Item3
					)
				);
			});
		}

		return tupleDataDtos;
	}
}
