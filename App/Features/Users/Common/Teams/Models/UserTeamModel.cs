using App.Features.Teams.Common.Models;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Teams.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.Teams.Models;

public class UserTeamModel : IUserTeamModel
{
	[Key]
	[Required]
	public string MemberId { get; set; }

	[Required]
	[ForeignKey(nameof(MemberId))]
	public virtual UserModel? Member { get; set; }

	[Key]
	[Required]
	public Guid TeamId { get; set; }

	[Required]
	[ForeignKey(nameof(TeamId))]
	public virtual TeamModel? Team { get; set; }

	public UserTeamModel(string memberId, Guid teamId)
	{
		MemberId = memberId;
		TeamId = teamId;
	}
}
