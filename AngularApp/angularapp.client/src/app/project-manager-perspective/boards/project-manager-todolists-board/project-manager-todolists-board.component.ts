import { Component } from '@angular/core';

@Component({
  selector: 'app-project-manager-todolists-board',
  templateUrl: './project-manager-todolists-board.component.html',
  styleUrl: './project-manager-todolists-board.component.css'
})

export class ProjectManagerTodolistsBoardComponent {

  todolists: Array<TodoListCard>;
  todoListImgPath : string;
  avatarPath : string;

  constructor() {

    this.todoListImgPath = "/assets/other/list-puzzle.jpg";
    this.avatarPath = "/assets/avatars/bearAvatar.png";

    this.todolists = [
      {
        Title: "TodoList Title #1",
        TeamName: "Team Name Elo",
        TotalTasks: 30,
        CompletedTasks: 20,
        TeamAvatarPath: "",
        ProjectTitle: "Project Title",
        Description: "Lorem Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores"
      },
      {
        Title: "TodoList Title #2",
        TeamName: "Team Name Elo",
        TotalTasks: 30,
        CompletedTasks: 20,
        TeamAvatarPath: "",
        ProjectTitle: "Project Title",
        Description: "Lorem Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores"
      },
      {
        Title: "TodoList Title #3",
        TeamName: "Team Name Elo",
        TotalTasks: 30,
        CompletedTasks: 20,
        TeamAvatarPath: "",
        ProjectTitle: "Project Title",
        Description: "Lorem Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores"
      },
      {
        Title: "TodoList Title #4",
        TeamName: "Team Name Elo",
        TotalTasks: 30,
        CompletedTasks: 20,
        TeamAvatarPath: "",
        ProjectTitle: "Project Title",
        Description: "Lorem Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores Ipsum dolores"
      }      
    ];
  }

  GoToTodoListCreation() : void {

  }
}


interface TodoListCard {
  Title: string;
  TeamName: string;
  TotalTasks: number;
  CompletedTasks: number;
  TeamAvatarPath: string;
  ProjectTitle: string;
  Description: string;
}