namespace Project_Main.Infrastructure.Helpers
{
    public static class CustomRoutes
    {
        public const string LoginPath = "/[controller]/[action]";
        public const string ErrorHandlingPath = "/Shared/Error";
        public const string AccessDeniedPath = "/Shared/AccessDenied";
        public const string DefaultRouteName = "default";
        public const string DefaultRoutePattern = "{controller=Account}/{action=Login}/{id?}";

        public const string TodoListControllerRoute = TodoListCtrl.Name;
        public const string AccountControllerRoute = AccountCtrl.Name;

		public const string MainBoardRouteName = "MainBoard";
        public const string MainBoardRoute = "[controller]/[action]";

        public const string AllDetailsRoute = "[controller]/[action]";
        public const string TodoListDetailsRoute = "Boards/Details/[controller]/{id:int}";
        public const string TodoListCreateRoute = "[controller]/[action]";
		public const string TodoListEditRoute = "[controller]/{id:int}/[action]";
        public const string TodoListDeleteRoute = "[controller]/{id:int}/[action]";
        public const string TodoListDeletePostRoute = "[controller]/{id:int}/[action]";
		public const string TodoListDuplicateRoute = "[controller]/{todoListId:int}/[action]";
        public const string LoginByProviderRoute = "[action]/{provider}";
        public const string TaskDetailsRoute = $"Boards/[action]/{TodoListCtrl.Name}/{{routeTodoListId:int}}/[controller]/{{routeTaskId:int}}";
        public const string CreateTaskRoute = TodoListCtrl.Name + "/{id:int}/[controller]/[action]";
        public const string CreateTaskPostRoute = TodoListCtrl.Name + "/{todoListId:int}/[controller]/[action]";
        public const string TaskEditRoute = TodoListCtrl.Name + "/{todoListId:int}/[controller]/{taskId:int}/[action]";
        public const string TaskDeleteGetRoute = TodoListCtrl.Name + "/{todoListId:int}/[controller]/{taskId:int}/[action]";
		public const string TaskDeletePostRoute = TodoListCtrl.Name + "/[controller]/[action]";
	}
}
