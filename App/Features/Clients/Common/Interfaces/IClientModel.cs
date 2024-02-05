using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Clients.Common.Interfaces;

public interface IClientModel
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
	public string Phone {  get; set; }

	[Required]
	public string UserId { get; set; }

	[Required]
	[ForeignKey(nameof(UserId))]
	public UserModel? User { get; set; }

	public ICollection<ProjectModel> Projects { get; set; }
}