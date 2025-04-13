import { TaskStatus } from "../enums/task-status.enum";
import { ITaskStatus } from "./task-status.model";

export default class TaskStatusHelper {

  private static taskStatuses : ITaskStatus[] = [
    { label: TaskStatus.NextToDo.toString(), value: TaskStatus.NextToDo.toString() },
    { label: TaskStatus.InProgress.toString(), value: TaskStatus.InProgress.toString() },
    { label: TaskStatus.Done.toString(), value: TaskStatus.Done.toString() },
    { label: TaskStatus.Abandoned.toString(), value: TaskStatus.Abandoned.toString() },
  ];

    static getSeverity(status: string) {
        switch (status.toUpperCase()) {
            case TaskStatus.Abandoned:
                return "danger";

            case TaskStatus.Done:
                return "success";

            case TaskStatus.NextToDo:
                return "info";

            case TaskStatus.InProgress:
                return "warning";

            default:
                return "";
        }
    }

    static getTaskStatuses() : ITaskStatus[]{
        return this.taskStatuses.slice();
    }
}
