import { ITodoList } from "../../../../common/models/todoList.model";

export interface IProjectTodoList extends ITodoList {
  teamName: string;
  teamLiderName: string;
  tasksCount: number;
  tasksCompleted: number;
  color: string;
  teamColor: string;
  chart: any;
}
