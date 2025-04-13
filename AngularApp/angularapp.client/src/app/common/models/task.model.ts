import { IRepresentative } from "./representative.model";
import { ITag, Tag } from './tag.model';

export interface ITask {
    id: string;
    title: string;
    shortDescription: string;
    description: string;
    teamMate: IRepresentative;
    status: string;
    daysLeft: number;
    deadline: string | Date;
    reminder: string | Date;
    tags: Array<ITag>;
}

export class Task implements ITask {

    id: string;
    title: string;
    shortDescription: string;
    description: string;
    teamMate: IRepresentative;
    status: string;
    daysLeft: number;
    deadline: string | Date;
    reminder: string | Date;
    tags: ITag[];

    constructor(
        id: string,
        title: string,
        shortDescription: string,
        description: string,
        teamMate: IRepresentative,
        status: string,
        daysLeft: number,
        deadline: string | Date,
        reminder: string | Date,
        tags: ITag[]
    ) {
        this.id = id
        this.title = title
        this.shortDescription = shortDescription
        this.description = description
        this.teamMate = teamMate
        this.status = status
        this.daysLeft = daysLeft
        this.deadline = deadline
        this.tags = tags
        this.reminder = reminder
    }

    public static createTaskModel(taskSource: ITask): ITask {
        let tempTags: ITag[] = [];
        taskSource.tags.forEach(tag => tempTags.push(Tag.createTagModel(tag)));

        return new Task(
            taskSource.id, taskSource.title, taskSource.shortDescription,
            taskSource.description, taskSource.teamMate, taskSource.status,
            taskSource.daysLeft, taskSource.deadline, taskSource.reminder, tempTags
        );
    }
}
