using App.Features.Boards;
using App.Features.Tasks;
using App.Features.TodoLists;
using App.Features.Users;

namespace App.Common;

public static class ControllersConsts
{
	public readonly struct UserCtrl
	{
		public const string Name = "User";
		public const string LoginByProviderAction = nameof(UserController.LoginByProvider);
		public const string RegisterAction = nameof(UserController.Register);
		public const string LoginAction = nameof(UserController.Login);
		public const string LogoutAction = nameof(UserController.LogOut);
		public const string ErrorAction = nameof(UserController.Error);
		public const string GoogleProvider = "google";
	}

	public readonly struct BoardsCtrl
	{
		public const string Name = "Boards";
		public const string BrieflyAction = nameof(BoardsController.Briefly);
		public const string AllAction = nameof(BoardsController.All);
	}

	public readonly struct TaskCtrl
	{
		public const string Name = "Task";
		public const string DetailsAction = nameof(TaskController.Show);
		public const string CreateAction = nameof(TaskController.Create);
		public const string EditGetAction = nameof(TaskController.Edit);
		public const string EditPostAction = nameof(TaskController.EditPost);
		public const string DeleteAction = nameof(TaskController.Delete);
		public const string DeletePostAction = nameof(TaskController.DeletePost);
	}

	public readonly struct TodoListCtrl
	{
		public const string Name = "TodoList";
		public const string CreateAction = nameof(TodoListController.Create);
		public const string EditAction = nameof(TodoListController.Edit);
		public const string DeleteAction = nameof(TodoListController.Delete);
		public const string DeletePostAction = nameof(TodoListController.DeletePost);
		public const string DuplicateAction = nameof(TodoListController.Duplicate);
		public const string ShowAction = nameof(TodoListController.Show);
	}
}
