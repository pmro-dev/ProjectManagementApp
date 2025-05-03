import { Injectable } from '@angular/core';
import Chart from 'chart.js/auto';
import { PmStatisticsBoardService } from './project-manager-statistics-board.service';

@Injectable({ providedIn: 'root' })
export class PmStatisticsChartsService
{
  public budgetChart: any;
  public tasksProgressChart: any;
  public todoListsProgressChart: any;
  public todoListTasksProgressChart: any;
  public appLogoPath: string = "/assets/other/appLogo.jpg";
  public userAvatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  public currentUserName: string = "Jan Kowalski";
  private budgetData: any;
  private tasksProgressData: any;
  private todoListsProgressData: any;

  constructor(statisticsService: PmStatisticsBoardService) {
    this.createCharts();
    this.budgetData = statisticsService.budgetData;
    this.tasksProgressData = statisticsService.tasksProgressData;
    this.todoListsProgressData = statisticsService.todoListsProgressData;
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
}
