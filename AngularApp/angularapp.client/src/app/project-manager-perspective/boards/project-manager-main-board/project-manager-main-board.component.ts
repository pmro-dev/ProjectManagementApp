import { Component } from '@angular/core';
import { Project, ProjectImages } from './project-manager-project/project.model';

@Component({
  selector: 'app-project-manager-main-board',
  templateUrl: './project-manager-main-board.component.html',
  styleUrl: './project-manager-main-board.component.css'
})

export class ProjectManagerMainBoardComponent {
  avatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  slackPath: string = "/assets/other/slack-img.png";
  teamsImgPath: string = "/assets/icons/teamIcon.jpg";
  listsImgPath: string = "/assets/icons2/noteIcon.jpg";
  tasksImgPath: string = "/assets/icons2/tasksIcon.jpg";
  progressImgPath: string = "/assets/icons2/progressIcon.jpg";

  projectImages: ProjectImages = {
    AvatarPath: "/assets/avatars/avatar1-mini.jpg",
    SlackPath: "/assets/other/slack-img.png",
    TeamsImgPath: "/assets/icons/teamIcon.jpg",
    ListsImgPath: "/assets/icons2/noteIcon.jpg",
    TasksImgPath: "/assets/icons2/tasksIcon.jpg",
    ProgressImgPath: "/assets/icons2/progressIcon.jpg",
  }

  projects: Array<Project> = [
    { Id: 0, Title: "Project 1", Count: 12 },
    { Id: 1, Title: "Project 2", Count: 35 },
    { Id: 2, Title: "Project 3", Count: 76 }
  ];

  constructor() {}
  GoToProjectCreation(): void { }
}


