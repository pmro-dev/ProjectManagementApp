using App.Common.Views;
using static App.Common.ControllersConsts;

namespace App.Common;

public static class CustomRoutes
{
    public const string LoginPath = "/[controller]/[action]";
    public const string ErrorHandlingPath = "/Shared/Error";
    public const string AccessDeniedPath = "/Shared/AccessDenied";
    public const string DefaultRouteName = "default";
    public const string DefaultRoutePattern = "{controller=User}/{action=Login}/{id?}";
	public const string ExceptionShowRoute = "{controller}/{action}";
    public static readonly string ExceptionHandlerPath = string.Concat("/", ExceptionCtrl.Name, "/", ExceptionCtrl.ShowAction, "/", ViewsConsts.ExceptionViews.Error);

	public const string TodoListControllerRoute = TodoListCtrl.Name;
    public const string UserControllerRoute = UserCtrl.Name;

    public const string MainBoardRoute = "/[controller]/[action]";
    public const string MainBoardUri = BoardsCtrl.Name + "/" + BoardsCtrl.BrieflyAction;

    public const string BoardAllRoute = "/[controller]/[action]";
    public const string TodoListShowRoute = "/Boards/[action]/[controller]/{id:int}";
    public const string TodoListCreateRoute = "/[controller]/[action]";
    public const string TodoListEditRoute = "/[controller]/{id:int}/[action]";
    public const string TodoListDeleteRoute = "/[controller]/{id:int}/[action]";
    public const string TodoListDeletePostRoute = "/[controller]/{id:int}/[action]";
    public const string TodoListDuplicateRoute = "/[controller]/{todoListId:int}/[action]";
    public const string LoginByProviderRoute = "/[action]/{provider}";
    public const string TaskShowRoute = $"/Boards/[action]/{TodoListCtrl.Name}/{{routeTodoListId:int}}/[controller]/{{routeTaskId:int}}";
    public const string CreateTaskRoute = TodoListCtrl.Name + "/{id:int}/[controller]/[action]";
    public const string CreateTaskPostRoute = TodoListCtrl.Name + "/{todoListId:int}/[controller]/[action]";
    public const string TaskEditGetRoute = TodoListCtrl.Name + $"/{{todoListId:int}}/{TaskCtrl.Name}/{{taskId:int}}/{TaskCtrl.EditGetAction}";
    public const string TaskEditPostRoute = TodoListCtrl.Name + $"/{{todoListId:int}}/{TaskCtrl.Name}/{{taskId:int}}/{TaskCtrl.EditPostAction}";
    public const string TaskDeleteGetRoute = TodoListCtrl.Name + "/{todoListId:int}/[controller]/{taskId:int}/[action]";
    public const string TaskDeletePostRoute = TodoListCtrl.Name + "/[controller]/[action]";
}
