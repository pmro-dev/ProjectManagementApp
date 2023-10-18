using Microsoft.AspNetCore.Authentication.Cookies;

namespace Project_Main.Infrastructure.Helpers
{
	/// <summary>
	/// Class contains constants values (the most) for authentication setup but also for database and user claims creation.
	/// </summary>
	public static class ConfigConstants
	{
		public const string ConnectionStringMainDb = "ConnectionStrings:TodoFinalAppDbConnection";
		public const string ConnectionStringIdentityDb = "ConnectionStrings:IdentityConnection";

		public const string AuthSchemeClaimValue = "nullClaimValue";

		#region Authentication Help

		public const string GoogleAuthority = "https://accounts.google.com";
		public const string AuthGoogleClientId = "AuthGoogle:ClientId";
		public const string AuthGoogleClientSecret = "AuthGoogle:ClientSecret";
		public const string GoogleEmailScope = "https://www.googleapis.com/auth/userinfo.email";
		public const string GoogleUrlToLogout = "https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:7103";
		public const string GoogleOpenIdCallBackPath = "/auth";
		public const string AuthSchemeClaimKey = ".AuthScheme";
		public const string DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		public const string DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		public const string GoogleOpenIDScheme = "google";
		public const string CustomCookieName = "ToDoCustomCookie";

		#endregion
	}


	#region Controllers Constants

	public readonly struct AccountCtrl
	{
		public const string Name = "Account";
		public const string LoginByProviderAction = "LoginByProvider";
		public const string RegisterAction = "Register";
		public const string LoginAction = "Login";
		public const string ErrorAction = "Error";
		public const string GoogleProvider = "google";
	}

	public readonly struct BoardsCtrl
	{
		public const string Name = "Boards";
		public const string BrieflyAction = "Briefly";
		public const string AllAction = "All";
		public const string SingleDetailsAction = "SingleDetails";
	}

	public readonly struct TaskCtrl
	{
		public const string Name = "Task";
		public const string DetailsAction = "Details";
		public const string CreateAction = "Create";
		public const string EditAction = "Edit";
		public const string DeleteAction = "Delete";
		public const string DeleteConfirmedAction = "DeleteConfirmed";
	}

	public readonly struct TodoListCtrl
	{
		public const string Name = "TodoList";
		public const string CreateAction = "Create";
		public const string EditAction = "Edit";
		public const string DeleteAction = "Delete";
		public const string DeleteConfirmedAction = "DeleteConfirmed";
		public const string DuplicateAction = "Duplicate";
	}

	#endregion


	#region Views Constants

	public readonly struct TaskViews
	{
		public const string FolderName = "Task";
		public const string Create = "CreateTask";
		public const string Delete = "DeleteTask";
		public const string Details = "DetailsTask";
		public const string Edit = "EditTask";
	}

	public readonly struct TodoListViews
	{
		public const string FolderName = "TodoList";
		public const string Create = "CreateTodoList";
		public const string Delete = "DeleteTodoList";
		public const string Edit = "EditTodoList";
	}

	public readonly struct BoardsViews
	{
		public const string FolderName = "Boards";
		public const string All = "All";
		public const string Briefly = "Briefly";
		public const string SingleDetails = "SingleDetails";
		public const string DeleteModal = "_DeleteModal";
	}

	public readonly struct AccountViews
	{
		public const string FolderName = "Account";
		public const string Login = "Login";
		public const string Register = "Register";
		public const string DefaultLayout = "_LoginLayout";
	}

	public readonly struct SharedViews
	{
		public const string FolderName = "Shared";
		public const string DefaultLayout = "_Layout";
		public const string TaskTable = "_TaskTableDetails";
		public const string TodoListTable = "_TodoListTable";
		public const string TodoListTableDetails = "_TodoListTableDetails";
		public const string Validation = "_ValidationScriptsPartial";
		public const string AccessDenied = "AccessDenied";
		public const string Error = "Error";
	}

	#endregion

}
