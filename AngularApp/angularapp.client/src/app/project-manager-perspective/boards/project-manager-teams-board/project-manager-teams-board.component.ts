import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-project-manager-teams-board',
  templateUrl: './project-manager-teams-board.component.html',
  styleUrl: './project-manager-teams-board.component.css'
})

export class ProjectManagerTeamsBoardComponent {
  Teams: Array<Team>;
  avatarPath: string;

  constructor() {

    this.Teams = [
      {
        Name: "Siema Elo Team",
        Members: [{ FullName: "Jan Kowalski", Position: "UX Designer", Color : `rgb(124, 255, 180)`}, { FullName: "Joanna Kowalska", Position: "HR", Color : `rgb(255, 200, 220)` }, { FullName: "Joanna Kowalska", Position: "Front-end Developer", Color: `rgb(240, 216, 187)` }, { FullName: "Joanna Kowalska", Position: "Technical Lider", Color : `rgb(215, 226, 175)` }, { FullName: "Joanna Kowalska", Position: "Analytic", Color : `rgb(195, 200, 235)` }],
        Color: "rgb(255, 216, 216)"
      },
      {
        Name: "Master Czułki",
        Members: [{ FullName: "Jan Kowalski", Position: "UX Designer", Color : `rgb(255, 216, 150)`}, { FullName: "Joanna Kowalska", Position: "HR", Color : `rgb(218, 190, 223)` }, { FullName: "Joanna Kowalska", Position: "Front-end Developer", Color : `rgb(213, 235, 221)` }, { FullName: "Joanna Kowalska", Position: "Technical Lider", Color : `rgb(245, 222, 245)` }, { FullName: "Joanna Kowalska", Position: "Analytic", Color : `rgb(167, 227, 198)` }],
        Color: "rgb(255, 254, 216)"
      },
      {
        Name: "Filantropia",
        Members: [{ FullName: "Jan Kowalski", Position: "UX Designer", Color : `rgb(212, 178, 45)`}, { FullName: "Joanna Kowalska", Position: "HR", Color : `rgb(145, 67, 250)` }, { FullName: "Joanna Kowalska", Position: "Front-end Developer", Color : `rgb(255, 216, 187)` }, { FullName: "Joanna Kowalska", Position: "Technical Lider", Color : `rgb(255, 216, 15)` }, { FullName: "Joanna Kowalska", Position: "Analytic", Color : `rgb(255, 120, 187)` }],
        Color: "rgb(216, 255, 229)"
      }
    ];

    this.avatarPath = "/assets/avatars/bearAvatar.png";
  }
}

interface Team {
  Name: string;
  Members: Array<Member>;
  Color: string;
}

interface Member {
  FullName: string;
  Position: string;
  Color: string;
}