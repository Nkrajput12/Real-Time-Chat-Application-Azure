import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User, ChatRoom } from '../models';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AdminService {
  private usersUrl = `${environment.apiUrl}/api/users`;
  private adminUrl = `${environment.apiUrl}/api/admin`;

  constructor(private http: HttpClient) {}

  // User methods
  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.adminUrl}/users`);
  }

  suspendUser(userId: number): Observable<any> {
    return this.http.put(`${this.adminUrl}/users/${userId}/suspend`, {});
  }

  deleteUser(userId: number): Observable<any> {
    return this.http.delete(`${this.adminUrl}/users/${userId}`);
  }

  // Room methods
  getAllRooms(): Observable<ChatRoom[]> {
    return this.http.get<ChatRoom[]>(`${this.adminUrl}/rooms/all-stats`);
  }

  forceDeleteRoom(roomId: number): Observable<any> {
    return this.http.delete(`${this.adminUrl}/rooms/force-delete/${roomId}`, { responseType: 'text' });
  }

  getAnalytics(): Observable<any> {
    return this.http.get(`${this.adminUrl}/analytics`);
  }

  getAuditLogs(): Observable<any[]> {
    return this.http.get<any[]>(`${this.adminUrl}/audit-logs`);
  }
}
