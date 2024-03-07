import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProjectManagerMainBoardComponent } from './project-manager-perspective/boards/project-manager-main-board/project-manager-main-board.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProjectManagerTodolistsBoardComponent } from './project-manager-perspective/boards/project-manager-todolists-board/project-manager-todolists-board.component';

@NgModule({
  declarations: [
    AppComponent,
    ProjectManagerMainBoardComponent,
    ProjectManagerTodolistsBoardComponent,
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
