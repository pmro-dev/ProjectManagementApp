namespace Project_Main.Infrastructure.Helpers
{
    public static class CustomRoutes
    {
        public const string LoginPath = "/Account/Login";
        public const string ErrorHandlingPath = "/Shared/Error";
        public const string AccessDeniedPath = "/Shared/AccessDenied";
        public const string DefaultRouteName = "default";
        public const string DefaultRoutePattern = "{controller=Account}/{action=Login}/{id?}";

        public const string TodoListControllerRoute = "TodoList";
        public const string AccountControllerRoute = "Account";

		public const string MainBoardRouteName = "MainBoard";
        public const string MainBoardRoute = "Boards/Briefly";

        public const string AllDetailsRoute = "All/Details";
        public const string TodoListDetailsRoute = "Boards/Details/TodoList/{id:int}";
        public const string TodoListCreateRoute = "[controller]/[action]";
        public const string TodoListDeleteRoute = TodoListControllerRoute + "/{id:int}/Delete";
        public const string TodoListDeletePostRoute = TodoListControllerRoute + "/{id:int}/DeletePost";
		public const string TodoListDuplicateRoute = TodoListControllerRoute + "/{todoListId:int}/Duplicate";
        public const string LoginByProviderRoute = "Login/{provider}";

        public const string TaskDetailsRoute = TodoListControllerRoute + "/{routeTodoListId:int}/[controller]/{routeTaskId:int}/Details";
        public const string CreateTaskRoute = TodoListControllerRoute + "/{id:int}/Create";
        public const string CreateTaskPostRoute = TodoListControllerRoute + "/{todoListId:int}/Create";
        public const string TaskEditRoute = TodoListControllerRoute + "/{todoListId:int}/[controller]/{taskId:int}/[action]";
        public const string TaskDeleteGetRoute = TodoListControllerRoute + "/{todoListId:int}/[controller]/{taskId:int}/[action]";
		public const string TaskDeletePostRoute = TodoListControllerRoute + "/[controller]/[action]";
	}
}
