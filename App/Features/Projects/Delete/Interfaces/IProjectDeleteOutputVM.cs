using App.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Projects.Delete.Interfaces;

public interface IProjectDeleteOutputVM
{
	Guid Id { get; set; }
	byte[] RowVersion { get; set; }
	string Title { get; set; }
	string Description { get; set; }
	string OwnerFullName { get; set; }
	string CreatorFullName { get; set; }

	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime Created { get; set; }

	ICollection<string> ClientsNames { get; set; }
}
