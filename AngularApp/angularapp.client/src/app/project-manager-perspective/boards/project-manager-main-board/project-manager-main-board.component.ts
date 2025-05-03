import { Component } from '@angular/core';
import { PmProjectsService } from './project-manager-project.service';
import { Project, ProjectImages } from './project-manager-project/project.model';

@Component({
  selector: 'app-project-manager-main-board',
  templateUrl: './project-manager-main-board.component.html',
  styleUrl: './project-manager-main-board.component.css',
  standalone: true,
})

export class ProjectManagerMainBoardComponent {
  projectImages: ProjectImages;
  projects: Array<Project> = [];

  constructor(projectsService: PmProjectsService)
  {
    this.projectImages = projectsService.getProjectImages();
    this.projects = projectsService.getProjects();
  }

  GoToProjectCreation(): void { }
}


