//using App.Features.TEMP.TodoLists.Interfaces;
//using App.Features.TodoLists.Common.Models;
//using App.Features.Users.Common.Models;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace App.Features.TEMP.TodoLists.Models;

//public class UserTodoListModel : IUserTodoListModel
//{
//    [Timestamp]
//    public byte[] RowVersion { get; set; } = { 1, 1, 1 };

//    [Required]
//    public string OwnerId { get; set; } = string.Empty;

//    [Required]
//    //[ForeignKey(nameof(OwnerId))]
//    public UserModel? Owner { get; set; }

//    [Required]
//    public Guid TodoListId { get; set; } = Guid.Empty;

//    [Required]
//    //[ForeignKey(nameof(TodoListId))]
//    public TodoListModel? TodoList { get; set; }
//}
