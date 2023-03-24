using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_DomainEntities.Helpers;

namespace Project_DomainEntities
{
	/// <summary>
	/// Model for To Do List.
	/// </summary>
	public class TodoListModel : BasicModelAbstract
	{
		/// <summary>
		/// Gets or Sets Key of the Model / Object.
		/// </summary>
		[Key]
		[Required]
		public override int Id { get; set; }

		/// <summary>
		/// Gets or sets To Do List's name.
		/// </summary>
		[Required]
		[MaxLength(AttributesHelper.NameMaxLength)]
		[MinLength(AttributesHelper.NameMinLength)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Owner id.
		/// </summary>
		[Required]
		public string UserId { get; set; } = string.Empty;

		/// <summary>
		/// Gets or Sets
		/// </summary>
		public List<TaskModel> Tasks { get; set; } = new();

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

				if (Name == todoList.Name)
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
		public bool IsTheSame(TodoListModel obj)
		{
			if (obj == null || !GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				if (Name == obj.Name)
				{
					if (Tasks is null && obj.Tasks is null)
					{
						return true;
					}
					else
					{
						if (Tasks is not null && obj.Tasks is not null)
						{
							return Tasks.Count == obj.Tasks.Count && Id == obj.Id;
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
			return HashCode.Combine(Name, Id);
		}
	}
}