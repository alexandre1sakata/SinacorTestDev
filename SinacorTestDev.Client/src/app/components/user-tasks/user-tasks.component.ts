import { Component } from '@angular/core';
import { UserTaskService } from '../../services/user-task.service';
import { UserTask } from '../../models/UserTask';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-tasks',
  templateUrl: './user-tasks.component.html',
  styleUrl: './user-tasks.component.css'
})
export class UserTasksComponent {

  statusTask = ["Pendente", "Iniciada", "Finalizada"];
  searchTaskname: string = '';
  userTasks: UserTask[] = [];

  constructor(
    private userTaskService: UserTaskService, 
    private router: Router
  ){
    this.loadTasks();
  }

  loadTasks(){
    this.userTaskService.getAllTasks().subscribe(data => {
      this.userTasks = data;
    })
  }

  changeStatus(userTask: UserTask) {
    let newStatus = userTask.status == 'pendente' ? "iniciada" : "finalizada";
    this.userTaskService.updateTaskStatus(userTask.id, newStatus).subscribe(() => {
      this.loadTasks();
    });
  }

  searchTaskByName(){
    if(this.searchTaskname == undefined) return;
    
    this.userTaskService.getTasksByName(this.searchTaskname).subscribe(data => {
      this.userTasks = data;
    });
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
