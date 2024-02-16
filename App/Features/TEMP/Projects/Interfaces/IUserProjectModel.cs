using System.ComponentModel.DataAnnotations;

namespace App.Features.TEMP.Projects.Interfaces;

public interface IUserProjectModel
{
    [Timestamp]
    byte[] RowVersion { get; set; }

    [Required]
    public string OwnerId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
}
