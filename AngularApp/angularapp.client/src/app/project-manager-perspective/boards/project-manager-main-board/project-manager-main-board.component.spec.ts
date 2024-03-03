import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectManagerMainBoardComponent } from './project-manager-main-board.component';

describe('ProjectManagerMainBoardComponent', () => {
  let component: ProjectManagerMainBoardComponent;
  let fixture: ComponentFixture<ProjectManagerMainBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectManagerMainBoardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProjectManagerMainBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
