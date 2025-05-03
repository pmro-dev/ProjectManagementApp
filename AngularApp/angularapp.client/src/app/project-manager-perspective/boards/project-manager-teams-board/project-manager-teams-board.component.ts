import { Component } from '@angular/core';
import { PmTeamsBoardService, Team } from './project-manager-teams-board.service';
import { CommonModule } from '@angular/common';
import { TeamCardHighlightDirective } from './directives/team-card-highlight.directive';

@Component({
  selector: 'app-project-manager-teams-board',
  templateUrl: './project-manager-teams-board.component.html',
  styleUrl: './project-manager-teams-board.component.css',
  standalone: true,
  imports: [ CommonModule, TeamCardHighlightDirective ]
})

export class ProjectManagerTeamsBoardComponent {
  Teams: Array<Team>;
  avatarPath: string;

  constructor(teamsService: PmTeamsBoardService) {
    this.Teams = teamsService.getTeams();
    this.avatarPath = "/assets/avatars/bearAvatar.png";
  }
}
