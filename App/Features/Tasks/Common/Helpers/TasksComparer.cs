using App.Features.Tasks.Common.Models;

namespace App.Features.Tasks.Common.Helpers;

/// <summary>
/// Class allows to compare object based on the same Task Model type.
/// </summary>
public class TasksComparer : IComparer<TaskDto>
{
    public int Compare(TaskDto? x, TaskDto? y)
    {
        if (x is null && y is null)
        {
            return 0;
        }

        if (x is null)
        {
            return -1;
        }

        if (y is null)
        {
            return 1;
        }

        return x.DueDate.CompareTo(y.DueDate);
    }
}
