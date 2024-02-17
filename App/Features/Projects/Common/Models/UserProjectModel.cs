using App.Features.Projects.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Projects.Common.Models;

public class UserProjectModel : IUserProjectModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    [Required]
    public string OwnerId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }

    public UserProjectModel(string ownerId, Guid projectId)
    {
        OwnerId = ownerId;
        ProjectId = projectId;
        RowVersion = new byte[] { 1, 1, 1 };
    }
}
