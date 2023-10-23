﻿using Project_DomainEntities;
using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.AppData
{
	///<inheritdoc />
	public interface ITodoListRepository : IGenericRepository<TodoListModel>
    {
		/// <summary>
		/// Get a specific ToDoList with details (where details are related data in other tables).
		/// </summary>
		/// <param name="id">Targeted list id.</param>
		/// <returns>ToDoList with details from Db.</returns>
		Task<TodoListModel?> GetWithDetailsAsync(int id);

		/// <summary>
		/// Get All ToDoLists with details (where details are related data in other tables).
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>All ToDoLists wiith details from Db.</returns>
		Task<IEnumerable<TodoListModel>> GetAllWithDetailsAsync(string userId);

		/// <summary>
		/// Duplicate a whole, specifc ToDoList with details in Db (where details are related data in other tables).
		/// </summary>
		/// <param name="id">Targeted list id.</param>
		/// <returns></returns>
		Task DuplicateWithDetailsAsync(int id);

		/// <summary>
		/// Check that any ToDoList with the same name already exists.
		/// </summary>
		/// <param name="name">Targeted name to check.</param>
		/// <returns>True when ToDoList with specified name already exists, otherwise false.</returns>
		Task<bool> DoesAnyExistWithSameNameAsync(string name);
    }
}
