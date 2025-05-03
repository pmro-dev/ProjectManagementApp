import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class PmTodoListsBoardService {

  public todolists: Array<TodoListCard>;

  constructor() {
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

  addTodolist(todolist: TodoListCard) {
    this.todolists.push(todolist);
  }

  getTodolists() {
    return this.todolists;
  }
}

export interface TodoListCard {
  Title: string;
  TeamName: string;
  TotalTasks: number;
  CompletedTasks: number;
  TeamAvatarPath: string;
  ProjectTitle: string;
  Description: string;
}
