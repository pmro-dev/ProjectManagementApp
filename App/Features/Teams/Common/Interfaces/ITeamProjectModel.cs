﻿using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Teams.Common.Interfaces;

public interface ITeamProjectModel
{
	[Key]
	[Required]
	Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	ProjectModel? Project { get; set; }

	[Key]
	[Required]
	Guid TeamId { get; set; }

	[ForeignKey(nameof(TeamId))]
	TeamModel? Team { get; set; }
}