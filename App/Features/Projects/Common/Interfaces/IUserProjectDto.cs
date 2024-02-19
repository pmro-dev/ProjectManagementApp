namespace App.Features.Projects.Common.Interfaces;

public interface IUserProjectDto
{
	Guid Id { get; set; }

	byte[] RowVersion { get; set; }

	string OwnerId { get; set; }

	Guid ProjectId { get; set; }
}
