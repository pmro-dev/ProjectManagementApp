import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProjectManagerMainBoardComponent } from './project-manager-perspective/boards/project-manager-main-board/project-manager-main-board.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProjectManagerTodolistsBoardComponent } from './project-manager-perspective/boards/project-manager-todolists-board/project-manager-todolists-board.component';
import { ProjectManagerTeamsBoardComponent } from './project-manager-perspective/boards/project-manager-teams-board/project-manager-teams-board.component';
import { TeamCardHighlightDirective } from './project-manager-perspective/boards/project-manager-teams-board/team-card-highlight.directive';
import { ProjectManagerStatisticsBoardComponent } from './project-manager-perspective/boards/project-manager-statistics-board/project-manager-statistics-board.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {MatButtonModule} from '@angular/material/button';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { TodolistCardTeamColorDirective } from './project-manager-perspective/boards/project-manager-statistics-board/todolist-card-team-color.directive';
import { TodolistCardColorDirective } from './project-manager-perspective/boards/project-manager-statistics-board/todolist-card-color.directive';
import { MatTooltipModule } from '@angular/material/tooltip';
import { TodolistBoardComponent } from './common-boards/todolist-board/todolist-board.component';
import { CommonModule } from '@angular/common';
import { CommonModuleModule } from './Common/modules/common.module';
import { HtmlRendererComponent } from './Common/html-renderer/html-renderer.component';

@NgModule({
    declarations: [
        AppComponent,
        ProjectManagerMainBoardComponent,
        ProjectManagerTodolistsBoardComponent,
        ProjectManagerTeamsBoardComponent,
        TeamCardHighlightDirective,
        ProjectManagerStatisticsBoardComponent,
        TodolistCardTeamColorDirective,
        TodolistCardColorDirective
    ],
    providers: [
        provideAnimationsAsync()
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule, HttpClientModule,
        AppRoutingModule, NgbModule,
        MatSlideToggleModule, BrowserAnimationsModule,
        MatProgressBarModule, MatSnackBarModule, MatButtonModule,
        MatTooltipModule,
        TodolistBoardComponent,
        CommonModule,
        CommonModuleModule,
        HtmlRendererComponent
    ]
})
export class AppModule { }
