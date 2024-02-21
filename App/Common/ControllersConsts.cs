using App.Features.Boards;
using App.Features.Exceptions;
using App.Features.Projects;
using App.Features.Tasks;
using App.Features.TodoLists;
using App.Features.Users;

namespace App.Common;

public static class ControllersConsts
{
	public const int FirstPageNumber = 1;
	public const int DefaultItemsPerPageCount = 5;
	public const int FirstOptionItemsPerPageCount = 10;
	public const int SecondOptionItemsPerPageCount = 15;
	public const int ThirdOptionItemsPerPageCount = 30;

	public readonly struct ExceptionCtrl
	{
		public const string Name = "Exception";
		public const string ShowAction = nameof(ExceptionController.Show);
	}

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

	public readonly struct ProjectCtrl
	{
		public const string Name = "Project";
		public const string CreateAction = nameof(ProjectController.Create);
		public const string EditAction = nameof(ProjectController.Edit);
		public const string DeleteAction = nameof(ProjectController.Delete);
		public const string DeletePostAction = nameof(ProjectController.DeletePost);
		public const string ShowAction = nameof(ProjectController.Show);

		public const string DeleteClientPostAction = nameof(ProjectController.DeleteClient);
		public const string DeleteTagPostAction = nameof(ProjectController.DeleteTag);

		public const string AddClientPostAction = nameof(ProjectController.AddClient);
		public const string AddTagPostAction = nameof(ProjectController.AddTag);
	}

	public readonly struct TagCtrl
	{
		public const string Name = "Tag";
		public const string CreateAction = nameof(TagController.Create);
		public const string EditAction = nameof(TagController.Edit);
		public const string DeleteAction = nameof(TagController.Delete);
		public const string DeletePostAction = nameof(TagController.DeletePost);
		public const string ShowAction = nameof(TagController.Show);
	}
}
