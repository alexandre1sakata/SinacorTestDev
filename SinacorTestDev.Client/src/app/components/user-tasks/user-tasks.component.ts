import { Component } from '@angular/core';
import { UserTaskService } from '../../services/user-task.service';
import { UserTask } from '../../models/UserTask';

@Component({
  selector: 'app-user-tasks',
  templateUrl: './user-tasks.component.html',
  styleUrl: './user-tasks.component.css'
})
export class UserTasksComponent {

  searchTaskname: string = '';

  userTasks: UserTask[] = [];

  constructor(private userTaskService: UserTaskService){
    this.loadTasks();
  }

  loadTasks(){
    this.userTaskService.getAllTasks().subscribe(data => {
      this.userTasks = data;
    })
  }

  changeStatus(userTask: UserTask) {
    let newStatus = userTask.status == 'pendente' ? "iniciada" : "finalizada";
    this.userTaskService.updateTaskStatus(userTask.id, newStatus).subscribe();
    this.loadTasks();
    location.reload();
  }

  searchTaskByName(){
    console.log('term', this.searchTaskname);
    
    if(this.searchTaskname == undefined) return;
    
    this.userTaskService.getTaskByName(this.searchTaskname).subscribe(data => {
      this.userTasks = data;
    });
  }

  getStatusList(){
    return ["Pendente", "Iniciada", "Finalizada"];
  }
}
