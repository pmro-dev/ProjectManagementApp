//using App.Features.TodoLists.Common.Models;
//using App.Features.Users.Common.Models;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace App.Features.TEMP.TodoLists.Interfaces;

//public interface IUserTodoListModel
//{
//    [Timestamp]
//    public byte[] RowVersion { get; set; }

//    [Required]
//    public string OwnerId { get; set; }

//    [Required]
//    //[ForeignKey(nameof(OwnerId))]
//    public UserModel? Owner { get; set; }

//    [Required]
//    public Guid TodoListId { get; set; }

//    [Required]
//    //[ForeignKey(nameof(TodoListId))]
//    public TodoListModel? TodoList { get; set; }
//}
