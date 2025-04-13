import { Component, Input } from '@angular/core';
import { Project, ProjectImages } from './project.model';

@Component({
  selector: 'app-project-manager-project',
  templateUrl: './project-manager-project.component.html',
  styleUrl: './project-manager-project.component.css'
})

export class ProjectManagerProjectComponent {
@Input() images: ProjectImages;
  @Input() project: Project;
}
