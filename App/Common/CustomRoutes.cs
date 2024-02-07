using App.Common.Views;
using static App.Common.ControllersConsts;

namespace App.Common;

public static class CustomRoutes
{
    public const string LoginPath = "/[controller]/[action]";
    public const string ErrorHandlingPath = "/Shared/Error";
    public const string AccessDeniedPath = "/Shared/AccessDenied";
    public const string DefaultRouteName = "default";
    public const string DefaultRoutePattern = "{controller=User}/{action=Login}/{guid?}";
	public const string ExceptionShowRoute = "{controller}/{action}";
    public static readonly string ExceptionHandlerPath = string.Concat("/", ExceptionCtrl.Name, "/", ExceptionCtrl.ShowAction, "/", ViewsConsts.ExceptionViews.Error);

	public const string TodoListControllerRoute = TodoListCtrl.Name;
    public const string UserControllerRoute = UserCtrl.Name;

    public const string MainBoardRoute = "/[controller]/[action]";
    public const string MainBoardUri = BoardsCtrl.Name + "/" + BoardsCtrl.BrieflyAction;

    public const string BoardAllRoute = "/[controller]/[action]";
    public const string TodoListShowRoute = "/Boards/[action]/[controller]/{id:guid}";
    public const string TodoListCreateRoute = "/[controller]/[action]";
    public const string TodoListEditRoute = "/[controller]/{id:guid}/[action]";
    public const string TodoListDeleteRoute = "/[controller]/{id:guid}/[action]";
    public const string TodoListDeletePostRoute = "/[controller]/{id:guid}/[action]";
    public const string TodoListDuplicateRoute = "/[controller]/{todoListId:guid}/[action]";
    public const string LoginByProviderRoute = "/[action]/{provider}";
    public const string TaskShowRoute = $"/Boards/[action]/{TodoListCtrl.Name}/{{routeTodoListId:guid}}/[controller]/{{routeTaskId:guid}}";
    public const string CreateTaskRoute = TodoListCtrl.Name + "/{id:guid}/[controller]/[action]";
    public const string CreateTaskPostRoute = TodoListCtrl.Name + "/{todoListId:guid}/[controller]/[action]";
    public const string TaskEditGetRoute = TodoListCtrl.Name + $"/{{todoListId:guid}}/{TaskCtrl.Name}/{{taskId:guid}}/{TaskCtrl.EditGetAction}";
    public const string TaskEditPostRoute = TodoListCtrl.Name + $"/{{todoListId:guid}}/{TaskCtrl.Name}/{{taskId:guid}}/{TaskCtrl.EditPostAction}";
    public const string TaskDeleteGetRoute = TodoListCtrl.Name + "/{todoListId:guid}/[controller]/{taskId:guid}/[action]";
    public const string TaskDeletePostRoute = TodoListCtrl.Name + "/[controller]/[action]";
}
