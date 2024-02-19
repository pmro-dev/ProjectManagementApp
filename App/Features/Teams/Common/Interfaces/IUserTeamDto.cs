namespace App.Features.Teams.Common.Interfaces;

public interface IUserTeamDto
{
	Guid Id { get; set; }

	byte[] RowVersion { get; set; }

	string MemberId { get; set; }

	Guid TeamId { get; set; }
}
