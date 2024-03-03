import { Component } from '@angular/core';

@Component({
  selector: 'app-project-manager-main-board',
  templateUrl: './project-manager-main-board.component.html',
  styleUrl: './project-manager-main-board.component.css'
})

export class ProjectManagerMainBoardComponent {
  avatarPath : string = "/assets/avatars/avatar1-mini.jpg";
  slackPath : string = "/assets/other/slack-img.png";
  teamsImgPath : string = "/assets/icons/teamIcon.jpg";
  listsImgPath : string = "/assets/icons2/noteIcon.jpg";
  tasksImgPath : string = "/assets/icons2/tasksIcon.jpg";
  progressImgPath : string = "/assets/icons2/progressIcon.jpg";

  projects : Array<{Title : string, Count : number}> = [ 
    {Title: "Project 1", Count: 12},
    {Title: "Project 2", Count: 35},
    {Title: "Project 3", Count: 76}
  ];

constructor(){
  let cos : Project = {Title : "", Count : 2};
  this.projects.push(cos);
}

  GoToProjectCreation() : void{}

}

interface Project {
  Title : string,
  Count : number
}