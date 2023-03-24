using System.ComponentModel.DataAnnotations;

namespace TODO_Domain_Entities
{
    public class TagModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public List<TaskTagModel> TaskTags { get; set; } = new List<TaskTagModel>();
    }
}
