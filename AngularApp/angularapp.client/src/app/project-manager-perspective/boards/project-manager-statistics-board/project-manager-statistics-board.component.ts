import { Component } from '@angular/core';
// import { ChartModule } from 'primeng/chart';
import Chart from 'chart.js/auto';
// import { MatProgressBarModule } from '@angular/material/progress-bar';
// import { MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-project-manager-statistics-board',
  templateUrl: './project-manager-statistics-board.component.html',
  styleUrl: './project-manager-statistics-board.component.css'
})
export class ProjectManagerStatisticsBoardComponent {
  public budgetChart: any;
  public tasksProgressChart: any;
  public todoListsProgressChart: any;
  public todoListTasksProgressChart: any;
  public todoLists: Array<TodoList> = [
    {  
      Title : "UX Design",
      TasksCount : 17,
      TasksCompleted : 6,
      TeamName : "Króliczki Charliego",
      TeamLiderName : "Jaś Fasola"
    },
    {
      Title : "Web Theme",
      TasksCount : 12,
      TasksCompleted : 9,
      TeamName : "Morele",
      TeamLiderName : "Angelika Prodiż"
    },
    {
      Title : "Event Makieta",
      TasksCount : 20,
      TasksCompleted : 15,
      TeamName : "Robaczki",
      TeamLiderName : "Ewelina Roszpunka"
    }
  ]

  ngOnInit(): void {
    this.createCharts();
  }

  private budgetData = {
    labels: [
      'Budget Spent',
      'Budget Left',
      'Over Budget'
    ],
    datasets: [{
      label: 'Budget',
      data: [25, 75, 10],
      backgroundColor: [
        'rgb(190, 148, 238)',
        'rgb(148, 238, 148)',
        'rgb(238, 148, 148)'
      ],
      hoverOffset: 4
    }]
  };

  private tasksProgressData = {
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

  private todoListsProgressData = {
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

  private todoListTasksProgressData = {
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

  createCharts() {

    this.budgetChart = new Chart("BudgetChart", {
      type: 'doughnut',
      data: this.budgetData,
      options: {
        aspectRatio: 1.8,
        plugins: {
          legend: {
            display: false
          }
        },        
      }
    });

    this.tasksProgressChart = new Chart("TasksProgressChart", {
      type: 'bar',
      data: this.tasksProgressData,
      options: {
        maintainAspectRatio: false,
        responsive: true,
        indexAxis: 'y',
        plugins: {
          title: {
            display: true,
            text: 'TASKS PROGRESS'
          },
          legend: {
            display: false
          }
        },
        scales: {
          x: {
            stacked: true,
            grid: {
              display: false
            },
            ticks: {
              display: false,           
            },
            border: {
              display: false
            },
            min: 0,
            max: 52,
          },
          y: {
            stacked: true,
            grid: {
              display: false
            },
            ticks: {
              display: false,
            },
            border: {
              display: false
            }
          }
        }
      }
    });

    this.todoListsProgressChart = new Chart("TodoListsProgressChart", {
      type: 'bar',
      data: this.todoListsProgressData,
      options: {
        maintainAspectRatio: false,
        responsive: true,
        indexAxis: 'y',
        plugins: {
          title: {
            display: true,
            text: 'TODOLISTS PROGRESS'
          },
          legend: {
            display: false
          }
        },
        scales: {
          x: {
            stacked: true,
            grid: {
              display: false
            },
            ticks: {
              display: false,           
            },
            border: {
              display: false
            },
            min: 0,
            max: 7,
          },
          y: {
            stacked: true,
            grid: {
              display: false
            },
            ticks: {
              display: false,
            },
            border: {
              display: false
            }
          }
        }
      }
    });
    
    this.todoListTasksProgressChart = new Chart("TodoListTasksProgressChart", {
      type: 'bar',
      data: this.todoListTasksProgressData,
      options: {
        maintainAspectRatio: false,
        responsive: true,
        indexAxis: 'y',
        plugins: {
          title: {
            display: true,
            text: 'TASKS PROGRESS'
          },
          legend: {
            display: false
          }
        },
        scales: {
          x: {
            stacked: true,
            grid: {
              display: false
            },
            ticks: {
              display: false,           
            },
            border: {
              display: false
            },
            min: 0,
            max: 52,
          },
          y: {
            stacked: true,
            grid: {
              display: false
            },
            ticks: {
              display: false,
            },
            border: {
              display: false
            }
          }
        }
      }
    });    
  }
}

interface TodoList {
  Title : string;
  TasksCount : number;
  TasksCompleted : number;
  TeamName : string;
  TeamLiderName : string;
}