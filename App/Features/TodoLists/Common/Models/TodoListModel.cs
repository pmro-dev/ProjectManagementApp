#region USINGS
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Helpers;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Tags;
using App.Features.Users.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Features.TodoLists.Common.Models;

/// <summary>
/// Model for To Do List.
/// </summary>
public class TodoListModel : ITodoListModel
{
	/// <summary>
	/// Gets or Sets Key of the Model / Object.
	/// </summary>
	[Key]
	[Required]
	public Guid Id { get; set; } = Guid.NewGuid();

	[Timestamp]
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	/// <summary>
	/// Gets or sets To Do List's name.
	/// </summary>
	[Required]
	[MaxLength(TodoListAttributesHelper.NameMaxLength)]
	[MinLength(TodoListAttributesHelper.NameMinLength)]
	public string Title { get; set; } = string.Empty;

	public string? CreatorId { get; set; } = string.Empty;
	//public UserModel? Creator { get; set; }

	/// <summary>
	/// Owner id.
	/// </summary>
	public string? OwnerId { get; set; } = string.Empty;
	//public UserModel? Owner { get; set; }

	[Required]
	public Guid ProjectId { get; set; } = Guid.Empty;

	[ForeignKey(nameof(ProjectId))]
	public ProjectModel? Project { get; set; }

	[Required]
	public Guid TeamId { get; set; } = Guid.Empty;

	[ForeignKey(nameof(TeamId))]
	public TeamModel? Team { get; set; }

	/// <summary>
	/// Gets or Sets
	/// </summary>
	public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
	public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
	public ICollection<TodoListTagModel> TodoListTags { get; set; } = new List<TodoListTagModel>();


	/// <summary>
	/// Compares properties of two To Do Lists and return result of that compare.
	/// </summary>
	/// <param name="obj">Second To Do List compare to.</param>
	/// <returns>Result of compare -> true if Names of objects and tasks numbers ( but not ids! ) are equal, otherwise false.</returns>
	public override bool Equals(object? obj)
	{
		if (obj == null || !GetType().Equals(obj.GetType()))
		{
			return false;
		}
		else
		{
			var todoList = (TodoListModel)obj;

			if (Title == todoList.Title)
			{
				if (Tasks is null && todoList.Tasks is null)
				{
					return true;
				}
				else
				{
					if (Tasks is not null && todoList.Tasks is not null)
					{
						return Tasks.Count == todoList.Tasks.Count;
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// Compares properties of two To Do Lists and return result of that compare.
	/// </summary>
	/// <param name="obj">Second To Do List compare to.</param>
	/// <returns>Result of compare -> true if Names of objects, tasks numbers and ids are equal, otherwise false.</returns>
	public bool IsTheSame(ITodoListModel obj)
	{
		if (obj == null || !GetType().Equals(obj.GetType()))
		{
			return false;
		}
		else
		{
			if (Title == obj.Title)
			{
				if (Tasks is null && obj.Tasks is null)
				{
					return true;
				}
				else
				{
					if (Tasks is not null && obj.Tasks is not null)
					{
						var tempTasks = Tasks;
						var tempObjTasks = obj.Tasks;

						return tempTasks.Count == tempObjTasks.Count && Id == obj.Id;
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// Create and sets Hash Code.
	/// </summary>
	/// <returns></returns>
	public override int GetHashCode()
	{
		return HashCode.Combine(Title, Id);
	}
}