import { Component } from '@angular/core';
import { PmTodoListsBoardService, TodoListCard } from './project-manager-todolists-board.service';

@Component({
  selector: 'app-project-manager-todolists-board',
  templateUrl: './project-manager-todolists-board.component.html',
  styleUrl: './project-manager-todolists-board.component.css',
  standalone: true,
})

export class ProjectManagerTodolistsBoardComponent {

  todolists: Array<TodoListCard>;
  todoListImgPath : string;
  avatarPath : string;

  constructor(TodoListsService: PmTodoListsBoardService) {
    this.todolists = TodoListsService.getTodolists();
    this.todoListImgPath = "/assets/other/list-puzzle.jpg";
    this.avatarPath = "/assets/avatars/bearAvatar.png";
  }

  public GoToTodoListCreation()
  {

  }
}
