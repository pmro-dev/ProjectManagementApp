import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgFor, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { CommonModuleModule } from '../../Common/modules/common.module';
import { HtmlRendererComponent } from '../../Common/html-renderer/html-renderer.component';

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
  imports: [MatFormFieldModule, MatInputModule, MatTableModule, MatSortModule, MatPaginatorModule, NgFor, MatButtonModule,
    MatIconModule, CommonModuleModule, HtmlRendererComponent, NgIf],
})

export class TodolistBoardComponent implements AfterViewInit {

  columnsData: Array<IItemsToDisplay> = [
    { columnType: "title", columnName: "Title" },
    { columnType: "shortDescription", columnName: "Short Description" },
    { columnType: "teamMateName", columnName: "TeamMate Name" },
    { columnType: "status", columnName: "Status" },
    { columnType: "daysLeft", columnName: "Days Left" },
    { columnType: "deadline", columnName: "Deadline" },
    { columnType: "tags", columnName: "Tags" }
  ];

  columnsToDisplay: string[] = ['title', 'shortDescription', 'teamMateName', 'status', 'daysLeft', 'deadline', 'tags'];
  columnsNamesToDisplay: string[] = ['Title', 'Short Description', 'TeamMate Name', 'Status', 'Days Left', 'Deadline', 'Tags'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  expandedElement: ITaskData | null;
  dataSource: MatTableDataSource<ITaskData>;
  public appLogoPath: string = "/assets/other/appLogo.jpg";
  public userAvatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  public currentUserName: string = "Jan Kowalski";

  public avatarPath: string = "/assets/avatars/avatar1-mini.jpg";;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor() {
    this.dataSource = new MatTableDataSource(this.tasksData);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  public tasksData: Array<ITaskData> = [
    {
      title: "Task 1",
      shortDescription: "Some task description short",
      description: "1 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 2",
      shortDescription: "Some task description short",
      description: "2 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 3",
      shortDescription: "Some task description short",
      description: "3 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 4",
      shortDescription: "Some task description short",
      description: "4 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 5",
      shortDescription: "Some task description short",
      description: "5 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 6",
      shortDescription: "Some task description short",
      description: "6 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 7",
      shortDescription: "Some task description short",
      description: "7 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 8",
      shortDescription: "Some task description short",
      description: "8 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 9",
      shortDescription: "Some task description short",
      description: "9 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
    {
      title: "Task 10",
      shortDescription: "Some task description short",
      description: "10 Lorem ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum ipsum lorem ipsum",
      teamMateName: this.generateHTMLElement(),
      status: TaskStatusType.NextToDo,
      daysLeft: 15,
      deadline: "15.04.2023",
      tags: ["First Tag", "Second Tag"]
    },
  ];

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  generateHTMLElement(text?: string): HTMLElement {

    let itemHTMLBox: HTMLElement;

    if (text == null) {
      itemHTMLBox = document.createElement('div');

      let childImg = document.createElement('img');
      childImg.setAttribute('src', '/assets/avatars/avatar1-mini.jpg');
      childImg.style.width = '40px';
      childImg.style.height = 'auto';
      itemHTMLBox.appendChild(childImg);

      let childP: HTMLElement = document.createElement('p');
      childP.innerText = "Izer Kućma";
      // childP.style.backgroundColor = 'red';

      itemHTMLBox.appendChild(childP);
    }
    else {
      itemHTMLBox = document.createElement('p');
      itemHTMLBox.innerText = 'text';
    }

    return itemHTMLBox;
  }

  isItString(val: any): boolean {
    if (typeof val == 'object') { 
      return false;
    }
    else {
      return true;
    }
  }
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
  teamMateName: HTMLElement;
  status: TaskStatusType;
  daysLeft: number;
  deadline: string;
  tags: Array<string>;
}

export interface ITodoListData {
  title: string;
  description: string;
  tasks: Array<ITaskData>;
  projectTitle: string;
  teamName: string;
}

interface IItemsToDisplay {
  columnType: string,
  columnName: string
}