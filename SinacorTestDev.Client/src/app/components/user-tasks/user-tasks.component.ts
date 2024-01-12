import { Component } from '@angular/core';
import { UserTaskService } from '../../services/user-task.service';
import { UserTask } from '../../models/UserTask';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-user-tasks',
  templateUrl: './user-tasks.component.html',
  styleUrl: './user-tasks.component.css'
})
export class UserTasksComponent {

  statusTask = ["Pendente", "Iniciada", "Finalizada"];
  searchTaskname: string = '';
  userTasks$!: Observable<UserTask[]>;

  constructor(
    private userTaskService: UserTaskService, 
    private router: Router
  ){
    this.loadTasks();
  }

  loadTasks(){
    this.userTaskService.getAllTasks().subscribe({
      next: (data) => { 
        this.userTasks$ = of(data);
      },
      error: (ex) => { 
        console.error(ex);
        alert('Erro ao carregar tarefas!')
      }
    });
  }

  changeStatus(userTask: UserTask) {
    let newStatus = userTask.status == 'pendente' ? "iniciada" : "finalizada";

    this.userTaskService.updateTaskStatus(userTask.id, newStatus).subscribe({
      next: () => { 
        setTimeout( () => { this.loadTasks(); }, 1000 );
      },
      error: (ex) => { 
        console.error(ex);
        alert('Erro ao trocar status da Tarefa')
      }
    });
  }

  searchTaskByName(){
    if(this.searchTaskname == undefined) return;
    
    this.userTaskService.getTasksByName(this.searchTaskname).subscribe({
      next: (data) => { 
        this.userTasks$ = of(data)
      },
      error: (ex) => { 
        console.error(ex);
        alert('Erro ao consultar tarefas!')
        this.loadTasks();
      }
    });
  }

  getStatusList(){
    return this.statusTask;
  }

  removeTask(id: number, name: string){
    const accept = confirm(`Dejesa excluir a tarefa:  ${name} ?`);
    if (accept) {
      this.userTaskService.deleteTask(id).subscribe({
        next: () => { 
          alert('Tarefa excluída!')
          this.loadTasks();
        },
        error: (ex) => { 
          console.error(ex);
          alert('Erro ao excluir tarefa!')
        }
      });
    }
  }

  redirectToCreateTask() {
    this.router.navigate(['/create-task']);
  }
}
