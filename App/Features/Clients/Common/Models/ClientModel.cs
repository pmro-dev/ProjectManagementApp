#region USINGS
using App.Features.Clients.Common.Interfaces;
using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace App.Features.Clients.Common.Models;

public class ClientModel : IClientModel
{
	[Required]
	[Key]
	public Guid Id { get; set; }

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

	public ClientModel(string userId, string companyName, string jobTitle, string phone)
	{
		Id = Guid.NewGuid();
		UserId = userId;
		CompanyName = companyName;
		JobTitle = jobTitle;
		Phone = phone;

		Projects = new List<ProjectModel>();
	}
}
