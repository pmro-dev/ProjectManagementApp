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
  public avatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  public todoLists: Array<TodoList> = [
    {
      Title: "UX Design",
      Color: "rgb(236, 240, 250)",
      TasksCount: 17,
      TasksCompleted: 6,
      TeamName: "Króliczki Charliego",
      TeamLiderName: "Jaś Fasola",
      TeamColor: "purple",
      Chart: null
    },
    {
      Title: "Web Theme",
      Color: "rgb(236, 250, 238)",
      TasksCount: 12,
      TasksCompleted: 9,
      TeamName: "Morele",
      TeamLiderName: "Angelika Prodiż",
      TeamColor: "green",
      Chart: null
    },
    {
      Title: "Event Makieta",
      Color: "rgb(245, 236, 250)",
      TasksCount: 20,
      TasksCompleted: 15,
      TeamName: "Robaczki",
      TeamLiderName: "Ewelina Roszpunka",
      TeamColor: "yellow",
      Chart: null
    }
  ]

  ngOnInit(): void {
    this.createCharts();
  }

  ngAfterViewInit(): void {
    let temp: string;

    this.todoLists.forEach(todolist => {
      temp = todolist.Title + "Chart";
      todolist.Chart = this.createTodoListTasksChart(temp, this.todoListTasksProgressData, "TodoLists Tasks Progress", 6)
    });
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
        'rgb(148, 238, 148)',
        'rgb(190, 148, 238)',
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


  createTodoListTasksChart(id: string, data: any, title: string, max: number): Chart {
    return new Chart(id, {
      type: 'bar',
      data: data,
      options: {
        maintainAspectRatio: false,
        responsive: true,
        indexAxis: 'y',
        plugins: {
          title: {
            display: false,
            text: title
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
            max: max,
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
  }

  ReadMoreTodoLists(){
    console.log("WORKING READ MORE!")
  }
}

interface TodoList {
  Title: string;
  Color: string;
  TasksCount: number;
  TasksCompleted: number;
  TeamName: string;
  TeamLiderName: string;
  TeamColor: string;
  Chart: any;
}