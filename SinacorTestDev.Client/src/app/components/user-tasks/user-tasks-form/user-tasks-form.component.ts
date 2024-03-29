import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserTaskService } from '../../../services/user-task.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-user-tasks-form',
  templateUrl: './user-tasks-form.component.html',
  styleUrl: './user-tasks-form.component.css'
})
export class UserTasksFormComponent implements OnInit {

  taskForm!: FormGroup;
  isEditTask: boolean = false;
  title: string = 'Cadastrando nova tarefa';
  nameBtnSubmit: string = 'Cadastrar';

  constructor(
    private formBuilder: FormBuilder, 
    private userTaskService: UserTaskService,
    private router: Router,
    private activRoute: ActivatedRoute
  ){
    this.taskForm = this.formBuilder.group({
      id: [0],
      name: ['', Validators.required],
      description: ['', Validators.required],
      status: ['pendente', Validators.required],
    })
   }

  ngOnInit(): void {
    this.activRoute.params.subscribe((params) => {
      const taskId = params['id'];
      if (taskId) {
        this.isEditTask = true;
        this.title = 'Editando tarefa';
        this.nameBtnSubmit = 'Salvar';

        this.userTaskService.getTaskById(taskId).subscribe(data => {
          this.taskForm.patchValue({
            id: data.id,
            name: data.name,
            description: data.description,
            status: data.status
          });
        });

        this.userTaskService.getTaskById(taskId).subscribe({
          next: (data) => { 
            this.taskForm.patchValue({
              id: data.id,
              name: data.name,
              description: data.description,
              status: data.status
            });
          },
          error: (ex) => { 
            console.error(ex);
            alert('Erro ao carregar tarefa para editar!')
          }
        });
      }
    });
  }

  onSubmit(){
    if(this.taskForm.valid){
      let submitSuccess = false;
      
      if(!this.isEditTask){
        this.userTaskService.createTask(this.taskForm.value).subscribe({
          next: () => { this.processResultSubmit(true) },
          error: (ex) => { this.processResultSubmit(false, ex) }
        });        
      } else {
        this.userTaskService.updateTask(this.taskForm.value.id, this.taskForm.value).subscribe({
          next: () => { this.processResultSubmit(true) },
          error: (ex) => { this.processResultSubmit(false, ex) }
        });
      }
    }
  }

  processResultSubmit(submitSuccess: boolean, ex: any = null) {
    if (submitSuccess) {
      alert(`Tarefa ${this.isEditTask ? 'salva' : 'criada'}!`);
      setTimeout(() => { this.router.navigateByUrl('/tasks'); }, 1000);
    } else {
      console.error(ex)
      alert(`Erro ao ${this.isEditTask ? 'salvar' : 'criar'} tarefa!`);
    }
  }
}
