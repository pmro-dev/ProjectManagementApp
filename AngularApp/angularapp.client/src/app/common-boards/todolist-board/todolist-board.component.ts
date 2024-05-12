import { Component, ElementRef, HostBinding, Renderer2, ViewChild } from '@angular/core';
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
import { CalendarModule } from 'primeng/calendar';
import { ChipModule } from 'primeng/chip';
import { TagsDialogComponent } from '../../Common/Components/Dialogs/tags-dialog/tags-dialog.component';
import { ITaskModel, TaskModel } from '../../Common/Models/TaskModel';
import { ITagModel } from '../../Common/Models/TagModel';
import { IRepresentativeModel } from '../../Common/Models/RepresentativeModel';
import TaskStatusHelper, { ITaskStatus } from '../../Common/Models/TaskStatusHelper';
import { TaskDataSourceService } from './taskDataSourceService';

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
    HtmlRendererComponent, NgIf, TableModule, MultiSelectModule, FormsModule, ReactiveFormsModule, TagModule,
    DropdownModule, ButtonModule, InputTextModule, DatePipe, CalendarModule, ChipModule, TagsDialogComponent],
  providers: [TaskDataSourceService]
})

export class TodolistBoardComponent {

  appLogoPath: string = "/assets/other/appLogo.jpg";
  userAvatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  currentUserName: string = "Jan Kowalski";
  avatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  todoListName: string = "Current TodoList Name";
  loading: boolean = true;
  activityValues: number[] = [0, 100];
  mobileMenuViaManager: ElementRef;
  isMenuShow: boolean;
  selectedTeamMate: IRepresentativeModel;
  selectedTaskTagsToRemove: number[] = [];
  showDialog: boolean = false;
  taskForDialog: ITaskModel;
  teamMates: IRepresentativeModel[];
  tasksData: ITaskModel[];
  taskStatuses: ITaskStatus[];
  clonedTask: ITaskModel;
  datePick: Date | string;
  @HostBinding('class.custom-highlight') IsReminderTurn: boolean = false;
  @ViewChild("calendar", { static: false }) calendarChild: any;
  calendar: any;
  customButton: HTMLButtonElement;
  private reminderDateTemp: Date | string;

  constructor(private taskDataSourceService: TaskDataSourceService, private elementRef: ElementRef, private renderer: Renderer2) {
    this.teamMates = taskDataSourceService.getTeamMates();
    this.tasksData = taskDataSourceService.getData();
    this.taskStatuses = TaskStatusHelper.getTaskStatuses();
    this.createCustomHTMLElements();
  }

  createCustomHTMLElements() {
    this.customButton = document.createElement('button');
    this.customButton.className = "reminderButton";
    this.customButton.innerHTML = 'Reminder';
    this.customButton.style.color = "red";
    this.customButton.style.fontWeight = "700";

    this.customButton.addEventListener('mouseenter', () => {
      this.customButton.style.backgroundColor = "rgb(255, 0, 0, 0.04)";
    });

    this.customButton.addEventListener('mouseleave', () => {
      this.customButton.style.backgroundColor = '';
    });

    this.customButton.addEventListener('focus', () => {
      this.customButton.style.boxShadow = "0 0 0 2px #ffffff, 0 0 0 4px rgb(255, 0, 0, 0.4), 0 1px 2px 0 black";
    });

    this.customButton.addEventListener('click', () => {
      this.IsReminderTurn = true;
      this.datePick = new Date(this.reminderDateTemp);
    });    
  }

  onDatePickerShow(taskIn: ITaskModel) {
    const buttonsBar = this.elementRef.nativeElement.querySelector(".p-datepicker-buttonbar");

    this.renderer.appendChild(buttonsBar, this.customButton);
    this.renderer.addClass(this.customButton, "p-button");
    this.renderer.addClass(this.customButton, "p-button-text");
    this.reminderDateTemp = taskIn.reminder;
  }

  onDateSelect(taskIn: ITaskModel) {

    if (this.IsReminderTurn) {
      this.IsReminderTurn = false;
      taskIn.reminder = this.datePick;
    }
    else {
      taskIn.deadline = this.datePick;
    }

    console.log("Reminder date = " + taskIn.reminder);
    console.log("Deadline date = " + taskIn.deadline);
  }

  onCalendarClose(deadline: Date) {
    this.IsReminderTurn = false;
    this.datePick = deadline;
  }

  onBodyWallClick() {
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
  }

  clear(table: Table, inputField: HTMLInputElement) {
    table.clear();
    inputField.value = ""
  }

  getSeverity(status: string): string {
    return TaskStatusHelper.getSeverity(status);
  }

  onChipRemove(taskData: ITaskModel, tagToRemove: ITagModel) {
    taskData.tags = taskData.tags.filter(tag => tag.id != tagToRemove.id).slice();
  }

  onRowEditInit(task: ITaskModel) {
    this.clonedTask = TaskModel.createTaskModel(task);
    this.datePick = task.deadline;
  }

  onRowEditSave(taskIn: ITaskModel) {
    this.taskDataSourceService.updateTaskData(taskIn);

    let index = this.tasksData.findIndex(task => task.id == taskIn.id);
    this.tasksData[index] = taskIn;
  }

  onRowEditCancel(taskIn: ITaskModel, index: number) {
    this.tasksData[index] = this.clonedTask;
  }

  onSeeMore(source: ITaskModel) {
    this.taskForDialog = source;
    console.log("SEE MORE CLICKED");
    this.showDialog = true;
  }

  onVisibilityChange(newValue: boolean) {
    this.showDialog = newValue;
  }
}