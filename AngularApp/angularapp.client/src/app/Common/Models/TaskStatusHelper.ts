export enum TaskStatusType {
    NextToDo = "NEXT TODO",
    InProgress = "IN PROGRESS",
    Done = "DONE",
    Abandoned = "ABANDONED"
}

export default class TaskStatusHelper {

    static getSeverity(status: string) {
        switch (status.toUpperCase()) {
            case TaskStatusType.Abandoned:
                return "danger";

            case TaskStatusType.Done:
                return "success";

            case TaskStatusType.NextToDo:
                return "info";

            case TaskStatusType.InProgress:
                return "warning";

            default:
                return "";
        }
    }

    static getTaskStatuses() : ITaskStatus[]{
        return this.taskStatuses.slice();
    }

    private static taskStatuses : ITaskStatus[] = [
        { label: TaskStatusType.NextToDo.toString(), value: TaskStatusType.NextToDo.toString() },
        { label: TaskStatusType.InProgress.toString(), value: TaskStatusType.InProgress.toString() },
        { label: TaskStatusType.Done.toString(), value: TaskStatusType.Done.toString() },
        { label: TaskStatusType.Abandoned.toString(), value: TaskStatusType.Abandoned.toString() },
    ];    
}

export interface ITaskStatus {
    label: string,
    value: string
}