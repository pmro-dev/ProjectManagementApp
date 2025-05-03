import { Injectable } from "@angular/core";
import { IProjectTodoList } from "./models/project-todolist.model";

@Injectable({ providedIn: 'root' })
export class PmStatisticsBoardService
{
  public avatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  public todoLists: Array<IProjectTodoList> = [];
  public teams: Array<ITeam> = [];

  constructor() {
    this.todoLists = this.initTodolists();
    this.teams = this.initTeams();
  }

  public getTodoLists(): Array<IProjectTodoList> {
    return this.todoLists;
  }

  public getTeams(): Array<ITeam> {
    return this.teams;
  }

  private initTodolists(): Array<IProjectTodoList> {
    return  [
      {
        title: "UX Design",
        description:"dadasda",
        projectTitle: "Project 1",
        tasks: [],
        tasksCount: 17,
        tasksCompleted: 6,
        teamName: "Króliczki Charliego",
        teamLiderName: "Jaś Fasola",
        color: "rgb(236, 240, 250)",
        teamColor: "purple",
        tags: [],
        chart: null
      },
      {
        title: "Web Theme",
        description:"dadasda",
        projectTitle: "Project 2",
        tasks: [],
        tasksCount: 12,
        tasksCompleted: 9,
        teamName: "Morele",
        teamLiderName: "Angelika Prodiż",
        color: "rgb(236, 250, 238)",
        teamColor: "green",
        tags: [],
        chart: null
      },
      {
        title: "Event Makieta",
        description:"dadasda",
        projectTitle: "Project 2",
        tasks: [],
        tasksCount: 20,
        tasksCompleted: 15,
        teamName: "Robaczki",
        teamLiderName: "Ewelina Roszpunka",
        teamColor: "yellow",
        color: "rgb(245, 236, 250)",
        tags: [],
        chart: null
      }
    ];
  }

  private initTeams(): Array<ITeam> {
    return [
      {
        Name: "Króliczki Charliego",
        MonthlyCost: 75000,
        Members: [
          { Name: "Joanna Dragan", AvatarPath: this.avatarPath }, { Name: "Elżbieta Bażant", AvatarPath: this.avatarPath },
          { Name: "Krzysztof Frankowski", AvatarPath: this.avatarPath }, { Name: "Kryspin Baptyst", AvatarPath: this.avatarPath },
          { Name: "Aniela Dzik", AvatarPath: this.avatarPath }, { Name: "Henry Otomaton", AvatarPath: this.avatarPath }
        ]
      },
      {
        Name: "Fata Morgana",
        MonthlyCost: 50000,
        Members: [
          { Name: "Joanna Dragan", AvatarPath: this.avatarPath }, { Name: "Joanna Dragan", AvatarPath: this.avatarPath },
          { Name: "Joanna Dragan", AvatarPath: this.avatarPath }, { Name: "Joanna Dragan", AvatarPath: this.avatarPath },
          { Name: "Joanna Dragan", AvatarPath: this.avatarPath }, { Name: "Joanna Dragan", AvatarPath: this.avatarPath }
        ]
      },
      {
        Name: "Bon Apetit",
        MonthlyCost: 30000,
        Members: [
          { Name: "Joanna Dragan", AvatarPath: this.avatarPath }, { Name: "Joanna Dragan", AvatarPath: this.avatarPath },
          { Name: "Joanna Dragan", AvatarPath: this.avatarPath }, { Name: "Joanna Dragan", AvatarPath: this.avatarPath },
          { Name: "Joanna Dragan", AvatarPath: this.avatarPath }, { Name: "Joanna Dragan", AvatarPath: this.avatarPath }
        ]
      },
    ];
  }

  public budgetData = {
    labels: [
      'Budget Spent',
      'Budget Left',
      'Over Budget'
    ],
    datasets: [{
      label: 'Budget',
      data: [25, 75, 10],
      backgroundColor: [
        'rgb(148, 238, 148)',
        'rgb(190, 148, 238)',
        'rgb(238, 148, 148)'
      ],
      hoverOffset: 4
    }]
  };

  public tasksProgressData = {
    labels: ['STATUS'],
    datasets: [
      {
        label: 'COMPLETED',
        data: [15],
        backgroundColor: 'rgb(148, 238, 148)',
        borderRadius: 2,
        barPercentage: 1,
        borderSkip: false
      },
      {
        label: 'IN PROGRESS',
        data: [12],
        backgroundColor: 'rgb(190, 148, 238)',
        borderRadius: 2,
        barPercentage: 1,
        borderSkip: false,
      },
      {
        label: 'TODO',
        data: [25],
        backgroundColor: 'rgb(238, 148, 148)',
        borderRadius: 2,
        barPercentage: 1,
        borderSkip: false
      }
    ]
  };

  public todoListsProgressData = {
    labels: ['STATUS'],
    datasets: [
      {
        label: 'COMPLETED',
        data: [2],
        backgroundColor: 'rgb(148, 238, 148)',
        borderRadius: 2,
        barPercentage: 1,
        borderSkip: false
      },
      {
        label: 'IN PROGRESS',
        data: [5],
        backgroundColor: 'rgb(190, 148, 238)',
        borderRadius: 2,
        barPercentage: 1,
        borderSkip: false,
      }
    ]
  };

  public todoListTasksProgressData = {
    labels: ['STATUS'],
    datasets: [
      {
        label: 'COMPLETED',
        data: [1],
        backgroundColor: 'rgb(148, 238, 148)',
        borderRadius: 2,
        barPercentage: 0.5,
        borderSkip: false
      },
      {
        label: 'IN PROGRESS',
        data: [2],
        backgroundColor: 'rgb(190, 148, 238)',
        borderRadius: 2,
        barPercentage: 0.5,
        borderSkip: false,
      },
      {
        label: 'TODO',
        data: [3],
        backgroundColor: 'rgb(238, 148, 148)',
        borderRadius: 2,
        barPercentage: 0.5,
        borderSkip: false
      }
    ]
  };
}
