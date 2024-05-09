import { IRepresentativeModel } from "./RepresentativeModel";
import { ITagModel } from "./TagModel";

export interface ITaskModel {
    id: string;
    title: string;
    shortDescription: string;
    description: string;
    teamMate: IRepresentativeModel;
    status: string;
    daysLeft: number;
    deadline: string | Date;
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
    }
}