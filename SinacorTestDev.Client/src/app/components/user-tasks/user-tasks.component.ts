import { Component } from '@angular/core';
import { UserTaskService } from '../../services/user-task.service';
import { UserTask } from '../../models/UserTask';

@Component({
  selector: 'app-user-tasks',
  templateUrl: './user-tasks.component.html',
  styleUrl: './user-tasks.component.css'
})
export class UserTasksComponent {

  userTasks!: UserTask[];

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

  getStatusList(){
    return ["Pendente", "Iniciada", "Finalizada"];
  }
}
