using System.ComponentModel.DataAnnotations;

namespace App.Common.Interfaces;

public interface IBasicModelWithTitle
{
	[Key]
    [Required]
    Guid Id { get; set; }

	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	string Title { get; set; }
}
