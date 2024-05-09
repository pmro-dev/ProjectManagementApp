import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ChipModule } from 'primeng/chip';
import { DialogModule } from 'primeng/dialog';
import { NgFor } from '@angular/common';
import { ITaskModel, TaskModel } from '../../../Models/TaskModel';
import { ITagModel, TagModel } from '../../../Models/TagModel';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-tags-dialog',
  templateUrl: './tags-dialog.component.html',
  styleUrl: './tags-dialog.component.css',
  standalone: true,
  imports: [DialogModule, ChipModule, ButtonModule, NgFor, InputTextModule]
})

export class TagsDialogComponent {
  @Input() show: boolean = false;
  @Input() dataSource: ITaskModel;
  private dataSourceCopy: ITaskModel;
  @Output() visibilityChange = new EventEmitter<boolean>();
  taskTitle: string;

  ngOnChanges(changes: SimpleChanges): void {
    // if (changes['dataSource'] && changes['dataSource'].previousValue !== undefined) {
    if (changes['dataSource']) {
      this.dataSourceCopy =  TaskModel.createTaskModel(this.dataSource);
      this.taskTitle = this.dataSourceCopy.title;
    }
  }

  onEnterPress(value: string){
    let nextId = this.dataSource.tags.length + 1;
    this.dataSource.tags.push(new TagModel(nextId, value));
  }

  onChipRemove(tagToRemove: ITagModel) {
    this.dataSource.tags = this.dataSource.tags.filter(tag => tag.id != tagToRemove.id);
  }

  onCancel() {
    this.show = false;
    this.visibilityChange.emit(false);
    this.dataSource.tags = this.dataSourceCopy.tags.map(tag => ({... tag}));
  }

  onSave() {
    this.dataSourceCopy.tags = this.dataSource.tags.map(tag => ({... tag}));
    this.show = false;
    this.visibilityChange.emit(false);
  }

  printData(source: ITagModel[]): void {
    console.log("printing")
    Array.from(source).forEach(function (value, index) { console.log(value.title); });
  }
}
