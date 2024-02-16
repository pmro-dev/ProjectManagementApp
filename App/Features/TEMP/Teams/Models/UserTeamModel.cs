using App.Features.TEMP.Teams.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace App.Features.TEMP.Teams.Models;

public class UserTeamModel : IUserTeamModel
{
    [Timestamp]
    public byte[] RowVersion { get; set; } = { 1, 1, 1 };

    [Required]
    public string MemberId { get; set; }

    [Required]
    public Guid TeamId { get; set; }

    public UserTeamModel(string memberId, Guid teamId)
    {
        MemberId = memberId;
        TeamId = teamId;
    }
}
