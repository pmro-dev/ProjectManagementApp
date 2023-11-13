namespace App.Common.Views;

public static class ViewsConsts
{
    public readonly struct TaskViews
    {
        //public const string FolderName = "Task";
        public const string Create = "Create";
        public const string Delete = "Delete";
        public const string Details = "Details";
        public const string Edit = "Edit";
    }

    public readonly struct TodoListViews
    {
        //public const string FolderName = "TodoList";
        public const string Create = "Create";
        public const string Delete = "Delete";
        public const string Edit = "Edit";
        public const string Details = "Details";
    }

    public readonly struct BoardsViews
    {
        //public const string FolderName = "Boards";
        public const string All = "All";
        public const string Briefly = "Briefly";
        public const string DeleteModal = "_DeleteModal";
    }

    public readonly struct UserViews
    {
        //public const string FolderName = "User";
        public const string Login = "Login";
        public const string Register = "Register";
        public const string DefaultLayout = "_LoginLayout";
    }

    public readonly struct SharedViews
    {
        //public const string FolderName = "Shared";
        public const string DefaultLayout = "_Layout";
        public const string TaskTableDetails = "_TaskTableDetails";
        public const string TodoListTable = "_TodoListTable";
        public const string TodoListTableDetails = "_TodoListTableDetails";
        public const string ValidationPartial = "_ValidationScriptsPartial";
        public const string AccessDenied = "AccessDenied";
        public const string Error = "Error";
    }
}
