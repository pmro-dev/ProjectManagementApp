import { ITask } from "./task.model";
import { ITag } from "./tag.model";

export interface ITodoList {
    title: string;
    description: string;
    tasks: Array<ITask>;
    projectTitle: string;
    tags: Array<ITag>;
}

export class TodoList implements ITodoList {
    title: string;
    description: string;
    tasks: ITask[];
    projectTitle: string;
    teamName: string;
    teamLiderName: string;
    tags: ITag[];

    constructor(
        title: string,
        description: string,
        tasks: ITask[],
        projectTitle: string,
        teamName: string,
        teamLiderName: string,
        tags: ITag[]
    ) {
        this.title = title
        this.description = description
        this.tasks = tasks
        this.projectTitle = projectTitle
        this.teamName = teamName
        this.teamLiderName = teamLiderName
        this.tags = tags
    }
}
