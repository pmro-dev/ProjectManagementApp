using System.ComponentModel.DataAnnotations;

namespace App.Features.Teams.Common.Interfaces;

public interface IUserTeamModel
{
    [Key]
    [Required]
    Guid Id { get; set; }

    [Timestamp]
    byte[] RowVersion { get; set; }

    [Required]
    public string MemberId { get; set; }

    [Required]
    public Guid TeamId { get; set; }
}
