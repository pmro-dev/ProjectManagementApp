using App.Features.Teams.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.Teams.Interfaces;

public interface IUserTeamModel
{
	[Key]
	[Required]
	public string MemberId { get; set; }

	[Required]
	[ForeignKey(nameof(MemberId))]
	public UserModel? Member { get; set; }

	[Key]
	[Required]
	public Guid TeamId { get; set; }

	[Required]
	[ForeignKey(nameof(TeamId))]
	public TeamModel? Team { get; set; }
}
