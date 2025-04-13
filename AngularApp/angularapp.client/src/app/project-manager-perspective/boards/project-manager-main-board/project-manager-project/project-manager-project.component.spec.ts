import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectManagerProjectComponent } from './project-manager-project.component';

describe('ProjectManagerProjectComponent', () => {
  let component: ProjectManagerProjectComponent;
  let fixture: ComponentFixture<ProjectManagerProjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectManagerProjectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProjectManagerProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
