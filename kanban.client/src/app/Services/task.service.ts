import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Task } from '../Components/dashboard/models';
import { environment } from '../../Environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllTasks() {
    return this.http.get<Task[]>(`${this.apiUrl}/task`);
  }

  updateTask(id: number, data: Partial<Task>) {
    return this.http.put(`${this.apiUrl}/task/${id}`, data);
  }
}
