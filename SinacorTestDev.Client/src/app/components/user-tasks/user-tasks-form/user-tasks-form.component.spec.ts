import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTasksFormComponent } from './user-tasks-form.component';

describe('UserTasksFormComponent', () => {
  let component: UserTasksFormComponent;
  let fixture: ComponentFixture<UserTasksFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserTasksFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserTasksFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
