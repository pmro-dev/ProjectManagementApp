import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectManagerTodolistsBoardComponent } from './project-manager-todolists-board.component';

describe('ProjectManagerTodolistsBoardComponent', () => {
  let component: ProjectManagerTodolistsBoardComponent;
  let fixture: ComponentFixture<ProjectManagerTodolistsBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectManagerTodolistsBoardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProjectManagerTodolistsBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
