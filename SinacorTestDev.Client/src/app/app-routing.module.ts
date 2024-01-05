import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserTasksFormComponent } from './components/user-tasks/user-tasks-form/user-tasks-form.component';
import { UserTasksComponent } from './components/user-tasks/user-tasks.component';

const routes: Routes = [
  { path: 'create-task', component: UserTasksFormComponent},
  { path: 'tasks', component: UserTasksComponent },
  { path: '', redirectTo: '/tasks', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
