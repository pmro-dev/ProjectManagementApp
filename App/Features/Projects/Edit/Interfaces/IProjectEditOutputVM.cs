﻿using App.Common.Helpers;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Users.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Projects.Edit.Interfaces;

public interface IProjectEditOutputVM
{
	Guid Id { get; set; }

	string Title { get; set; }

    string Description { get; set; }

    Guid BudgetId { get; set; }

	ICollection<UserModel> ActualClients { get; set; } 
	ICollection<ProjectUserModel> ProjectClients { get; set; }
	ICollection<UserModel> OtherClients { get; set; }

	string OwnerId { get; set; }

    ProjectStatusType Status { get; set; }

	SelectList? StatusSelector { get; set; }

	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    DateTime Deadline { get; set; }

    ICollection<TagModel> Tags { get; set; }
}
