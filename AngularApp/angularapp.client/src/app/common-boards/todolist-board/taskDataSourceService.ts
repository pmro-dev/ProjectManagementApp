import { Injectable } from '@angular/core';
import { ITagModel } from '../../Common/Models/TagModel';
import { ITaskModel, TaskModel } from '../../Common/Models/TaskModel';
import { IRepresentativeModel } from '../../Common/Models/RepresentativeModel';
import { TaskStatusType } from '../../Common/Models/TaskStatusHelper';

@Injectable()
export class TaskDataSourceService {

    private teamMates: IRepresentativeModel[] = [];

    private tagsData: Array<ITagModel> = [
        { id: 1, title: "First Tag" },
        { id: 2, title: "Second Tag" },
        { id: 3, title: "Third Tag" },
        { id: 4, title: "Fourth Tag" },
        { id: 5, title: "Fifth Tag" },
        { id: 6, title: "Sixth Tag" },
    ];

    private dataSource: Array<ITaskModel> = [
        {
            id: "1",
            title: "Task 1",
            shortDescription: "Some task description short",
            description: "1 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Grzegorz Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.NextToDo.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[0], this.tagsData[1], this.tagsData[2], this.tagsData[3]]
        },
        {
            id: "2",
            title: "Task 2",
            shortDescription: "Some task description short",
            description: "2 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Jan Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.Done.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[0], this.tagsData[2]]
        },
        {
            id: "3",
            title: "Task 3",
            shortDescription: "Some task description short",
            description: "3 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Marek Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.Abandoned.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[2], this.tagsData[3]]
        },
        {
            id: "4",
            title: "Task 4",
            shortDescription: "Some task description short",
            description: "4 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Krzysztof Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.InProgress.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[4]]
        },
        {
            id: "5",
            title: "Task 5",
            shortDescription: "Some task description short",
            description: "5 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Elżbieta Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.NextToDo.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[5]]
        },
        {
            id: "6",
            title: "Task 6",
            shortDescription: "Some task description short",
            description: "6 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Maroni Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.NextToDo.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[3], this.tagsData[4]]
        },
        {
            id: "7",
            title: "Task 7",
            shortDescription: "Some task description short",
            description: "7 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Jusuf Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.NextToDo.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[1], this.tagsData[3], this.tagsData[4], this.tagsData[5]]
        },
        {
            id: "8",
            title: "Task 8",
            shortDescription: "Some task description short",
            description: "8 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Neli Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.NextToDo.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[0], this.tagsData[1], this.tagsData[3]]
        },
        {
            id: "9",
            title: "Task 9",
            shortDescription: "Some task description short",
            description: "9 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Potato Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.NextToDo.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[0], this.tagsData[2]]
        },
        {
            id: "10",
            title: "Task 10",
            shortDescription: "Some task description short",
            description: "10 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
            teamMate: { name: "Lincz Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
            status: TaskStatusType.NextToDo.toString(),
            daysLeft: 15,
            deadline: '2015-09-13',
            tags: [this.tagsData[1], this.tagsData[2]]
        }
    ];

    constructor() {
        this.dataSource.forEach((task) => {
            this.teamMates.push(task.teamMate);
            task.deadline = new Date(<Date>task.deadline)
        });
    }

    getData() {
        return this.dataSource.map(task => ({ ...task }));
    }

    getSingle(index: any): ITaskModel {
        if (typeof index == 'number') {
            return this.dataSource.map(task => ({ ...task }))[index];
        }
        else if (typeof index == 'string') {
            let temp = this.dataSource.find(task => task.id == index);

            if (temp != null) {
                return TaskModel.createTaskModel(temp);
            }
        }

        throw Error("ERROR wrong index | id to be able to get single task!");
    }

    getTeamMates() {
        return this.teamMates.slice();
    }

    updateTaskData(taskIn: ITaskModel) {
        let index = this.dataSource.findIndex(task => task.id == taskIn.id);

        if (index == null) {
            console.log("ERROR - task was not found on UpdateTaskData")
        }

        taskIn.daysLeft = this.getDayDiff(new Date(), <Date>taskIn.deadline);

        this.dataSource[index] = taskIn;
    }

    private getDayDiff(startDate: Date, endDate: Date): number {
        const msInDay = 24 * 60 * 60 * 1000;

        return Math.round(
            Math.abs(Number(endDate) - Number(startDate)) / msInDay
        );
    }
}