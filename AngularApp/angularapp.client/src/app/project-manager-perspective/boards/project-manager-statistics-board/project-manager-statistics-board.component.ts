import { Component } from '@angular/core';
import { IProjectTodoList } from './models/project-todolist.model';
import { PmStatisticsBoardService } from './project-manager-statistics-board.service';
import { TodolistCardColorDirective } from './directives/todolist-card-color.directive';
import { TodolistCardTeamColorDirective } from './directives/todolist-card-team-color.directive';
import { MatTooltipModule } from '@angular/material/tooltip';
// import { ChartModule } from 'primeng/chart';
// import { MatProgressBarModule } from '@angular/material/progress-bar';
// import { MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-project-manager-statistics-board',
  templateUrl: './project-manager-statistics-board.component.html',
  styleUrl: './project-manager-statistics-board.component.css',
  standalone: true,
  imports: [ TodolistCardColorDirective, TodolistCardTeamColorDirective, MatTooltipModule ]
})
export class ProjectManagerStatisticsBoardComponent {
  public budgetChart: any;
  public tasksProgressChart: any;
  public todoListsProgressChart: any;
  public todoListTasksProgressChart: any;
  public appLogoPath: string = "/assets/other/appLogo.jpg";
  public userAvatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  public currentUserName: string = "Jan Kowalski";
  public avatarPath: string = "";
  public todoLists: Array<IProjectTodoList> = [];
  public teams: Array<ITeam> = [];
  public membersCount: number = 0;

  constructor(statisticsService: PmStatisticsBoardService)
  {
    this.todoLists = statisticsService.getTodoLists();
    this.teams = statisticsService.getTeams();
    this.avatarPath = statisticsService.avatarPath;
  }

  ngOnInit(): void {
    this.membersCount = this.countMembers();
  }

  ngAfterViewInit(): void {
    // let temp: string;
    // this.todoLists.forEach(todolist => {
    //   temp = todolist.title + "Chart";
    //   todolist.chart = this.createTodoListTasksChart(temp, this.todoListTasksProgressData, "TodoLists Tasks Progress", 6)
    // });
  }

  countMembers(): number {
    let tempCount = 0;
    this.teams.forEach(team => tempCount = tempCount + team.Members.length);
    return tempCount;
  }

  ReadMoreTodoLists() {
    console.log("WORKING READ MORE TODOLISTS!")
  }

  ReadMoreTeams() {
    console.log("WORKING READ MORE TEAMS!")
  }
}
