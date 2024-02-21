using System.ComponentModel.DataAnnotations;

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectUserModel
{
    [Key]
    [Required]
    Guid Id { get; set; }

    [Timestamp]
    byte[] RowVersion { get; set; }

    [Required]
    public string OwnerId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
}
