using App.Common.Views;
using static App.Common.ControllersConsts;

namespace App.Common;

public static class CustomRoutes
{
    public const string ErrorHandlingPath = "/Shared/Error";
    public const string AccessDeniedPath = "/Shared/AccessDenied";
    public const string DefaultRouteName = "default";
    public const string DefaultRoutePattern = "{controller=User}/{action=Login}/{guid?}";
	public const string ExceptionShowRoute = "{controller}/{action}";
    public static readonly string ExceptionHandlerPath = string.Concat("/", ExceptionCtrl.Name, "/", ExceptionCtrl.ShowAction, "/", ViewsConsts.ExceptionViews.Error);

    public const string UserControllerRoute = UserCtrl.Name;


	#region Board Routes

	public const string MainBoardRoute = "/[controller]/[action]";
    public const string MainBoardUri = BoardsCtrl.Name + "/" + BoardsCtrl.BrieflyAction;
    public const string BoardAllRoute = "/[controller]/[action]";
	#endregion


	#region TodoList Routes

	public const string TodoListControllerRoute = TodoListCtrl.Name;
	public const string TodoListShowRoute = "/Boards/[action]/[controller]/{id:guid}";
    public const string TodoListCreateRoute = "/[controller]/[action]";
    public const string TodoListEditRoute = "/[controller]/{id:guid}/[action]";
    public const string TodoListDeleteRoute = "/[controller]/{id:guid}/[action]";
    public const string TodoListDeletePostRoute = "/[controller]/{id:guid}/[action]";
    public const string TodoListDuplicateRoute = "/[controller]/{todoListId:guid}/[action]";
	#endregion


	#region Task Routes

    public const string TaskShowRoute = $"/Boards/[action]/{TodoListCtrl.Name}/{{routeTodoListId:guid}}/[controller]/{{routeTaskId:guid}}";
    public const string CreateTaskRoute = TodoListCtrl.Name + "/{id:guid}/[controller]/[action]";
    public const string CreateTaskPostRoute = TodoListCtrl.Name + "/{todoListId:guid}/[controller]/[action]";
    public const string TaskEditGetRoute = TodoListCtrl.Name + $"/{{todoListId:guid}}/{TaskCtrl.Name}/{{taskId:guid}}/{TaskCtrl.EditGetAction}";
    public const string TaskEditPostRoute = TodoListCtrl.Name + $"/{{todoListId:guid}}/{TaskCtrl.Name}/{{taskId:guid}}/{TaskCtrl.EditPostAction}";
    public const string TaskDeleteGetRoute = TodoListCtrl.Name + "/{todoListId:guid}/[controller]/{taskId:guid}/[action]";
    public const string TaskDeletePostRoute = TodoListCtrl.Name + "/[controller]/[action]";
	#endregion


	#region Login Routes

	public const string LoginPath = "/[controller]/[action]";
	public const string LoginByProviderRoute = "/[action]/{provider}";
	#endregion


	#region Project Routes

	public const string ProjectControllerRoute = ProjectCtrl.Name;
	//public const string ProjectShowRoute = "/Boards/[action]/[controller]/{projectId:guid}";
	public const string ProjectShowTodoListsBoardRoute = "/[controller]/{projectId:guid}/Board/[action]";
	public const string ProjectShowStatisticsBoardRoute = "/[controller]/{projectId:guid}/Board/[action]";
	public const string ProjectShowTeamsBoardRoute = "/[controller]/{projectId:guid}/Board/[action]";
	public const string ProjectCreateRoute = "/[controller]/[action]";
	public const string ProjectEditRoute = "/[controller]/{projectId:guid}/[action]";

	public const string ProjectDeleteRoute = "/[controller]/{projectId:guid}/[action]";
	public const string ProjectDeletePostRoute = "/[controller]/{id:guid}/[action]";
	public const string ProjectDeleteClientPostRoute = "/[controller]/{projectId:guid}/[action]/{clientId:string}";

	public const string ProjectAddTagPostRoute = "/[controller]/{projectId:guid}/[action]";

	#endregion


	#region Team Routes

	private const string _SchemesPrefix = "schemes";
	private const string _ProjectsPrefix = "projects";

	public const string CreateTeamScheme = $"/[controller]/{_SchemesPrefix}/[action]";
	public const string CreateTeamWithinProjectScope = $"/{_ProjectsPrefix}/{{projectId:guid}}/[controller]/[action]";
	public const string EditTeamWithinProjectScope = $"/{_ProjectsPrefix}/{{projectId:guid}}/[controller]/{{teamId:guid}}/[action]";
	public const string EditTeamScheme = $"/[controller]/{_SchemesPrefix}/{{teamId:guid}}/[action]";

	public const string DeleteTeamWithinProjectScope = $"/{_ProjectsPrefix}/{{projectId:guid}}/[controller]/{{teamId:guid}}/[action]";
	public const string DeleteTeamScheme = $"/[controller]/{_SchemesPrefix}/{{teamId:guid}}/[action]";
	public const string ShowTeamsSchemes = $"/[controller]/{_SchemesPrefix}/[action]";
	public const string ShowTeamsOfProject = $"{_ProjectsPrefix}/{{projectId:guid}}/[controller]/{{teamId:guid}}/[action]";

	#endregion
}
