import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserTask } from '../models/UserTask';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserTaskService {

  readonly apiUrl = `${environment.apiUrl}/UserTask`;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private httpClient: HttpClient) { }

  getAllTasks(): Observable<UserTask[]> {
    return this.httpClient.get<UserTask[]>(this.apiUrl)
  }

  getTaskByName(id: number): Observable<UserTask> {
    return this.httpClient.get<UserTask>(`${this.apiUrl}/${id}`);
  }

  createTask(userTask: UserTask): Observable<UserTask> {
    return this.httpClient.post<UserTask>(this.apiUrl, JSON.stringify(userTask), this.httpOptions);
  }

  updateTask(id: number, userTask: UserTask): Observable<UserTask> {
    return this.httpClient.put<UserTask>(`${this.apiUrl}/${id}`, JSON.stringify(userTask), this.httpOptions);
  }

  deleteTask(id: number): Observable<any> {
    return this.httpClient.delete<UserTask>(`${this.apiUrl}/${id}`);
  }

  updateTaskStatus(id: number, newStatus: string) : Observable<any> {
    return this.httpClient.put<UserTask>(`${this.apiUrl}/ChangeStatus/${id}/${newStatus}`, this.httpOptions);
  }
}
