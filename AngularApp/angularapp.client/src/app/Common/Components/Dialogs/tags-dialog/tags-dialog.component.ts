import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ChipModule } from 'primeng/chip';
import { DialogModule } from 'primeng/dialog';
import { NgFor } from '@angular/common';
import { ITask, Task } from '../../../models/task.model';
import { InputTextModule } from 'primeng/inputtext';
import { ITag, Tag } from '../../../Models/tag.model';

@Component({
  selector: 'app-tags-dialog',
  templateUrl: './tags-dialog.component.html',
  styleUrl: './tags-dialog.component.css',
  standalone: true,
  imports: [DialogModule, ChipModule, ButtonModule, NgFor, InputTextModule]
})

export class TagsDialogComponent {
  @Input() show: boolean = false;
  @Input() dataSource: ITask;
  private dataSourceCopy: ITask;
  @Output() visibilityChange = new EventEmitter<boolean>();
  taskTitle: string;

  ngOnChanges(changes: SimpleChanges): void {
    // if (changes['dataSource'] && changes['dataSource'].previousValue !== undefined) {
    if (changes['dataSource']) {
      this.dataSourceCopy =  Task.createTaskModel(this.dataSource);
      this.taskTitle = this.dataSourceCopy.title;
    }
  }

  onEnterPress(value: string){
    let nextId = this.dataSource.tags.length + 1;
    this.dataSource.tags.push(new Tag(nextId, value));
  }

  onChipRemove(tagToRemove: ITag) {
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

  printData(source: ITag[]): void {
    console.log("printing")
    Array.from(source).forEach(function (value, index) { console.log(value.title); });
  }
}
