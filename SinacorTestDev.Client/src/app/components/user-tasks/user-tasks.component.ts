import { Component } from '@angular/core';
import { UserTaskService } from '../../services/user-task.service';
import { UserTask } from '../../models/UserTask';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

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
    this.userTasks$ = this.userTaskService.getAllTasks();
  }

  changeStatus(userTask: UserTask) {
    let newStatus = userTask.status == 'pendente' ? "iniciada" : "finalizada";
    
    this.userTaskService.updateTaskStatus(userTask.id, newStatus).subscribe(() => {
      setTimeout( () => { this.loadTasks(); }, 1000 );
    });
  }

  searchTaskByName(){
    if(this.searchTaskname == undefined) return;
    
    this.userTasks$ = this.userTaskService.getTasksByName(this.searchTaskname);
  }

  getStatusList(){
    return this.statusTask;
  }

  removeTask(id: number, name: string){
    const accept = confirm(`Dejesa excluir a task:  ${name} ?`);
    if (accept) {
      this.userTaskService.deleteTask(id).subscribe(() => {
        alert('Task excluída!')
        this.loadTasks();
      });
    }
  }

  redirectToCreateTask() {
    this.router.navigate(['/create-task']);
  }
}
