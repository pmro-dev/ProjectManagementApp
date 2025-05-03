import { Injectable } from "@angular/core";
import { Project, ProjectImages } from './project-manager-project/project.model';

@Injectable({ providedIn: 'root' })
export class PmProjectsService {

  constructor() { }

  getProjects(): Array<Project>
  {
    return this.projects;
  }

  getProjectImages(): ProjectImages
  {
    return this.projectImages;
  }

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
}
