import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectManagerTeamsBoardComponent } from './project-manager-teams-board.component';

describe('ProjectManagerTeamsBoardComponent', () => {
  let component: ProjectManagerTeamsBoardComponent;
  let fixture: ComponentFixture<ProjectManagerTeamsBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectManagerTeamsBoardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProjectManagerTeamsBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
