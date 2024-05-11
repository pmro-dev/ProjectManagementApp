import { IRepresentativeModel } from "./RepresentativeModel";
import { ITagModel, TagModel } from './TagModel';

export interface ITaskModel {
    id: string;
    title: string;
    shortDescription: string;
    description: string;
    teamMate: IRepresentativeModel;
    status: string;
    daysLeft: number;
    deadline: string | Date;
    reminder: string | Date;
    tags: Array<ITagModel>;
}

export class TaskModel implements ITaskModel {

    id: string;
    title: string;
    shortDescription: string;
    description: string;
    teamMate: IRepresentativeModel;
    status: string;
    daysLeft: number;
    deadline: string | Date;
    reminder: string | Date;
    tags: ITagModel[];

    constructor(
        id: string,
        title: string,
        shortDescription: string,
        description: string,
        teamMate: IRepresentativeModel,
        status: string,
        daysLeft: number,
        deadline: string | Date,
        reminder: string | Date,
        tags: ITagModel[]
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

    public static createTaskModel(taskSource: ITaskModel): ITaskModel {
        let tempTags: ITagModel[] = [];
        taskSource.tags.forEach(tag => tempTags.push(TagModel.createTagModel(tag)));

        return new TaskModel(
            taskSource.id, taskSource.title, taskSource.shortDescription,
            taskSource.description, taskSource.teamMate, taskSource.status,
            taskSource.daysLeft, taskSource.deadline, taskSource.reminder, tempTags
        );
    }
}