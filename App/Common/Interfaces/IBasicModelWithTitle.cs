using System.ComponentModel.DataAnnotations;

namespace App.Common.Interfaces;

public interface IBasicModelWithTitle
{
    [Required]
	[Key]
    public Guid Id { get; set; }

	[Required]
	public string Title { get; set; }
}
