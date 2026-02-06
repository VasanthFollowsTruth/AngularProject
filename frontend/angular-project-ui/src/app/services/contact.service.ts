import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from '../models/contact.model';
import { environment } from '../../environments/environment';
import { PagedResult } from '../models/paged-result.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private baseUrl = `${environment.apiUrl}/contacts`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.baseUrl);
  }

  getById(id: number): Observable<Contact> {
    return this.http.get<Contact>(`${this.baseUrl}/${id}`);
  }

  create(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(this.baseUrl, contact);
  }

  update(id: number, contact: Contact): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, contact);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  getPaged(
  page: number,
  pageSize: number,
  sortBy?: string,
  order?: string
  ): Observable<PagedResult<Contact>> {

    let url =
      `${this.baseUrl}/paged?page=${page}&pageSize=${pageSize}`;

    if (sortBy) {
      url += `&sortBy=${sortBy}&order=${order}`;
    }

    return this.http.get<PagedResult<Contact>>(url);
  }
}
