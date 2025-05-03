import { Component, input, Input } from '@angular/core';
import { Project, ProjectImages } from './project.model';

@Component({
  selector: 'app-project-manager-project',
  templateUrl: './project-manager-project.component.html',
  styleUrl: './project-manager-project.component.css',
  standalone: true,
})

export class ProjectManagerProjectComponent {
  // @Input() images: ProjectImages;
  // @Input() project: Project;
  public images = input<ProjectImages>({AvatarPath: '', SlackPath: '', TeamsImgPath: '', ListsImgPath: '', TasksImgPath: '', ProgressImgPath: ''});
  public project = input<Project>({Id: 0, Title: '', Count: 0});
}
