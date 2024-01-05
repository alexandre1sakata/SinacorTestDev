import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserTaskService } from '../../../services/user-task.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-tasks-form',
  templateUrl: './user-tasks-form.component.html',
  styleUrl: './user-tasks-form.component.css'
})
export class UserTasksFormComponent {

  taskForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder, 
    private userTaskService: UserTaskService,
    private router: Router
  ){
    this.taskForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      status: ['pendente', Validators.required],
    })
  }

  onSubmit(){
    if(this.taskForm.valid){
      this.userTaskService.createTask(this.taskForm.value).subscribe();
      this.router.navigateByUrl('/tasks')
    }
  }

}
