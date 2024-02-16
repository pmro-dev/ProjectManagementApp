using System.ComponentModel.DataAnnotations;

namespace App.Features.TEMP.Teams.Interfaces;

public interface IUserTeamModel
{
    [Timestamp]
    byte[] RowVersion { get; set; }

    [Required]
    public string MemberId { get; set; }

    [Required]
    public Guid TeamId { get; set; }
}
