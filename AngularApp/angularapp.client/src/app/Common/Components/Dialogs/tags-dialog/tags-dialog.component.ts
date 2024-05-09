import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ChipModule } from 'primeng/chip';
import { DialogModule } from 'primeng/dialog';
import { NgFor } from '@angular/common';
import { ITaskModel } from '../../../Models/TaskModel';
import { ITagModel } from '../../../Models/TagModel';

@Component({
  selector: 'app-tags-dialog',
  templateUrl: './tags-dialog.component.html',
  styleUrl: './tags-dialog.component.css',
  standalone: true,
  imports: [DialogModule, ChipModule, ButtonModule, NgFor]
})

export class TagsDialogComponent {
  @Input() show: boolean = false;
  @Input() dataSource: ITaskModel;
  private dataSourceCopy: ITaskModel;
  @Output() visibilityChange = new EventEmitter<boolean>();
  selectedTaskTagsToRemove: number[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    // if (changes['dataSource'] && changes['dataSource'].previousValue !== undefined) {
    if (changes['dataSource']) {
      this.dataSourceCopy = { ...this.dataSource };
    }
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
