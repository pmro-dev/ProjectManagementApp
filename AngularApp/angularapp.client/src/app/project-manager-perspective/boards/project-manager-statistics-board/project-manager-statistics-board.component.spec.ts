import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectManagerStatisticsBoardComponent } from './project-manager-statistics-board.component';

describe('ProjectManagerStatisticsBoardComponent', () => {
  let component: ProjectManagerStatisticsBoardComponent;
  let fixture: ComponentFixture<ProjectManagerStatisticsBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectManagerStatisticsBoardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProjectManagerStatisticsBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
