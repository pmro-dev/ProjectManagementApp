import { Component, ElementRef, ViewChild } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { DatePipe, NgFor, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { CommonModuleModule } from '../../Common/modules/common.module';
import { HtmlRendererComponent } from '../../Common/html-renderer/html-renderer.component';
import { Table } from 'primeng/table';
import { TableModule } from 'primeng/table';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TagModule } from 'primeng/tag';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-todolist-board',
  templateUrl: './todolist-board.component.html',
  styleUrl: './todolist-board.component.css',
  animations: [
    trigger('detailExpand', [
      state('collapsed,void', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, NgFor, MatButtonModule, MatIconModule, CommonModuleModule,
    HtmlRendererComponent, NgIf, TableModule, MultiSelectModule, FormsModule, ReactiveFormsModule,
    TagModule, DropdownModule, ButtonModule, InputTextModule, DatePipe],
})

export class TodolistBoardComponent {

  statuses!: any[];
  loading: boolean = true;
  activityValues: number[] = [0, 100];
  mateTemp: Representative = { name: "Izer Kućma", image: "/assets/avatars/avatar1-mini.jpg" };
  public appLogoPath: string = "/assets/other/appLogo.jpg";
  public userAvatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  public currentUserName: string = "Jan Kowalski";
  public avatarPath: string = "/assets/avatars/avatar1-mini.jpg";;
  public todoListName: string = "Current TodoList Name";
  mobileMenuViaManager: ElementRef;
  isMenuShow: boolean;
  @ViewChild('innerBody') innerBody: ElementRef;

  onBodyWallClick(){
    this.isMenuShow = !this.isMenuShow;
  }

  onLeftMenuPush(elementRef: ElementRef) {
    this.mobileMenuViaManager = elementRef;
  }

  onMenuShowChange(isShowed: boolean) {
    this.isMenuShow = isShowed;
  }

  ngOnInit(): void {
    this.loading = false;
    this.tasksData.forEach((task) => (task.deadline = new Date(<Date>task.deadline)));
  }

  clear(table: Table, inputField: HTMLInputElement) {
    table.clear();
    inputField.value = ""
  }

  getSeverity(status: string) {
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

  onRowEditInit(task: ITaskData) { }
  onRowEditSave(task: ITaskData) { }
  onRowEditCancel(task: ITaskData, index: number) { }

  public teamMates = [
    { name: "1 Izer Kućma", image: "/assets/avatars/avatar1-mini.jpg" },
    { name: "Jan Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
    { name: "3 Izer Kućma", image: "/assets/avatars/avatar1-mini.jpg" },
    { name: "4 Izer Kućma", image: "/assets/avatars/avatar1-mini.jpg" },
  ];

  public taskStatuses = [
    { label: TaskStatusType.NextToDo.toString(), value: TaskStatusType.NextToDo.toString() },
    { label: TaskStatusType.InProgress.toString(), value: TaskStatusType.InProgress.toString() },
    { label: TaskStatusType.Done.toString(), value: TaskStatusType.Done.toString() },
    { label: TaskStatusType.Abandoned.toString(), value: TaskStatusType.Abandoned.toString() },
  ];

  public tasksData: ITaskData[] = [
    {
      title: "Task 1",
      shortDescription: "Some task description short",
      description: "1 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.NextToDo.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 2",
      shortDescription: "Some task description short",
      description: "2 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: { name: "Jan Kowalski", image: "/assets/avatars/avatar1-mini.jpg" },
      status: TaskStatusType.Done.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 3",
      shortDescription: "Some task description short",
      description: "3 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.Abandoned.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 4",
      shortDescription: "Some task description short",
      description: "4 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.InProgress.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 5",
      shortDescription: "Some task description short",
      description: "5 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.NextToDo.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 6",
      shortDescription: "Some task description short",
      description: "6 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.NextToDo.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 7",
      shortDescription: "Some task description short",
      description: "7 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.NextToDo.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 8",
      shortDescription: "Some task description short",
      description: "8 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.NextToDo.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 9",
      shortDescription: "Some task description short",
      description: "9 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.NextToDo.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["# First Tag", "# Second Tag"]
    },
    {
      title: "Task 10",
      shortDescription: "Some task description short",
      description: "10 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMate: this.mateTemp,
      status: TaskStatusType.NextToDo.toString(),
      daysLeft: 15,
      deadline: '2015-09-13',
      tags: ["# First Tag", "# Second Tag"]
    },
  ];
}

export interface Representative {
  name?: string;
  image?: string
}

export enum TaskStatusType {
  NextToDo = "NEXT TODO",
  InProgress = "IN PROGRESS",
  Done = "DONE",
  Abandoned = "ABANDONED"
}

export interface ITaskData {
  title: string;
  shortDescription: string;
  description: string;
  teamMate: Representative;
  status: string;
  daysLeft: number;
  deadline: string | Date;
  tags: Array<string>;
}

export interface ITodoListData {
  title: string;
  description: string;
  tasks: Array<ITaskData>;
  projectTitle: string;
  teamName: string;
}