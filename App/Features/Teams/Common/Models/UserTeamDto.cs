using App.Features.Teams.Common.Interfaces;

namespace App.Features.Teams.Common.Models;

public class UserTeamDto : IUserTeamDto
{
	public Guid Id { get; set; } = Guid.Empty;

	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public string MemberId { get; set; } = string.Empty;

	public Guid TeamId { get; set; } = Guid.Empty;
}
