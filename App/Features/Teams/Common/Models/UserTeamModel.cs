using App.Features.Teams.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Teams.Common.Models;

public class UserTeamModel : IUserTeamModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    [Required]
    public string MemberId { get; set; }

    [Required]
    public Guid TeamId { get; set; }

    public UserTeamModel(string memberId, Guid teamId)
    {
        MemberId = memberId;
        TeamId = teamId;
        RowVersion = new byte[] { 1, 1, 1 };
    }
}
