﻿using App.Common.ViewModels;
using App.Features.Tasks.Create.Models;
using MediatR;

namespace App.Features.Tasks.Create;

public class CreateTaskQuery : IRequest<WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>>
{
    public int TaskId { get; }

    public CreateTaskQuery(int taskId)
    {
        TaskId = taskId;
    }
}