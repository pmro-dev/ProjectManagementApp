using App.Features.Projects.Common.Interfaces;

namespace App.Features.Projects.Common.Models;

public class UserProjectDto : IUserProjectDto
{
	public Guid Id { get; set; } = Guid.Empty;

	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public string OwnerId { get; set; } = string.Empty;

	public Guid ProjectId { get; set; } = Guid.Empty;
}
