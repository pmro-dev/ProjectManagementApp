import { ITaskModel } from "./TaskModel";
import { ITagModel } from "./TagModel";

export interface ITodoListModel {
    title: string;
    description: string;
    tasks: Array<ITaskModel>;
    projectTitle: string;
    teamName: string;
    tags: Array<ITagModel>;
}

export class TodoListModel implements ITodoListModel {
    title: string;
    description: string;
    tasks: ITaskModel[];
    projectTitle: string;
    teamName: string;
    tags: ITagModel[];

    constructor(
        title: string,
        description: string,
        tasks: ITaskModel[],
        projectTitle: string,
        teamName: string,
        tags: ITagModel[]
    ) {
        this.title = title
        this.description = description
        this.tasks = tasks
        this.projectTitle = projectTitle
        this.teamName = teamName
        this.tags = tags
    }
}