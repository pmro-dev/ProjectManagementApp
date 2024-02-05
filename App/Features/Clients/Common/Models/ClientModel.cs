using App.Features.Clients.Common.Interfaces;
using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Clients.Common.Models;

public class ClientModel : IClientModel
{
	[Required]
	[Key]
	public int Id { get; set; }

	[Required]
	public string CompanyName { get; set; }

	[Required]
	public string JobTitle { get; set; }

	[Required]
	[Phone]
	public string Phone { get; set; }

	[Required]
	public string UserId { get; set; }

	[Required]
	[ForeignKey(nameof(UserId))]
	public virtual UserModel? User { get; set; }

	public ICollection<ProjectModel> Projects { get; set; }

	public ClientModel(string companyName, string jobTitle, string phone, string userId)
	{
		CompanyName = companyName;
		JobTitle = jobTitle;
		Phone = phone;
		UserId = userId;

		Projects = new HashSet<ProjectModel>();
	}
}
