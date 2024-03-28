import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TodolistBoardComponent } from './todolist-board.component';

describe('TodolistBoardComponent', () => {
  let component: TodolistBoardComponent;
  let fixture: ComponentFixture<TodolistBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TodolistBoardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TodolistBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
