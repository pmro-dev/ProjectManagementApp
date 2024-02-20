using App.Features.Projects.Delete.Interfaces;

namespace App.Features.Projects.Delete.Models;

public class ProjectDeleteOutputVM : IProjectDeleteOutputVM
{
	public Guid Id { get; set; } = Guid.Empty;
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };
	public string Title { get; set; } = string.Empty;
	public string Description { get ; set; } = string.Empty;
	public string OwnerFullName { get ; set; } = string.Empty;
	public string CreatorFullName { get ; set; } = string.Empty;
	public DateTime Created { get; set; } = DateTime.MinValue;
	public ICollection<string> ClientsNames { get ; set; } = new List<string>();
}
